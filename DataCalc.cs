using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;


namespace Queryabl
{
    /// <summary>
    /// Main class for extended functionality.
    /// Includes implementation of linq fluent clauses for "Where", "Select" 
    /// with relevent customizations.
    /// </summary>
    public static class DataCalc
    {
        /// <summary>
        /// NOTE: This is an example of how this should be done if implemented in code using methods (still Where(), Select() can be used).
        /// Allows for mixed generic enumerated data type filtering,
        /// retrieves and filters all numerical values only from the generic IEnumerable list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IEnumerable</param>
        /// <returns>Filtered generic IEnumerable list of values</returns>
        /// <example>
        /// Get the numbers from a mixed data type list.
        /// <code>
        /// var ienum_mixed = new List<object></object>{ 103,"vffsd","fmos",new List<string></string>{"gfdfs","fsd","3489"},9.0,89.3m,99.42m,1,90000000000};
        /// var ienum_nums = ienum_mixed.FilterNumerics();
        /// </code>
        /// </example>
        public static IEnumerable<T> FilterNumerics<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                if (IsNumericType(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// NOTE: This is an example of how this should be done if implemented in code using methods (still Where(), Select() can be used).
        /// Additional examples displaying extended usage on generic IEnumerables/IQueryables on mixed data types,
        /// uses anonymous function(s) for filtering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IEnumerable</param>
        /// <param name="predicate">Anonymous function used as a predicate-based filter</param>
        /// <returns>Filtered generic enumerable list of values</returns>
        /// <example>
        /// Get the numbers from a mixed data type list, simple examples with filters on queryable parts for
        /// extended numerical filtering on data cleanup operations.
        /// NOTE: FilterNumerics is used only for removing all non-numerical datatypes at once.
        /// <code>
        /// IQueryable<object></object> alldata = new List<object></object> { 103, "vffsd", "fmos", new List<string></string> { "gfdfs", "fsd", "3489" }, 9.0, 89.3m, 99.42m, 1, 90000000000 }.AsQueryable();
        /// var numericValues = alldata.FilterNumerics();
        /// //Sample Default Usage - With/Without FilterNumerics()
        /// // .Where(n => n is long); //select only long
        /// // .Where(n => n.ToString().Length > 10 == true); // select numeric values of prespecified length
        /// // .Where(n => n is UInt32); // select only uint
        /// // .Where(n => (n is Double) AND ((double)n >= 1)); //select only doubles 'AND' should be replaced with appropriate sign
        /// </code>
        /// </example>
        public static IEnumerable<T> FilterNumerics<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                predicate = (i) => IsNumericType(i);
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Retrieves an IEnumerable of generic collections that have an odd/even length
        /// and uses fluent syntax.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IEnumerable of enumerations</param>
        /// <returns>Filtered IEnumerable list of generic collections of odd or even lengths.</returns>
        /// <example>
        /// Filtering of nested lists based on their odd/even size.
        /// <code>
        /// var test_list = new <List></List><List></List> <int></int>{new List<int></int>{1,3,4,4},
        /// new List<int></int>{43,54,32},
        ///						  new List<int></int>{99,10,403,10,20,10},
        ///						  new List<int></int>{1},
        ///						  new List<int></int>{90,40} }.AsQueryable();
        ///						  
        /// test_list.CollEvenLength(); //returns all lists that have even size.
        /// test_list.CollEvenLength().Take(1); //returns first list of elements that has even size.
        /// test_list.CollEvenLength().Take(2));  //retruns two lists of elements that have even size.
        /// </code>
        /// </example>
        public static IEnumerable<IEnumerable<T>> CollEvenLength<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Where(innerSeq => innerSeq.OddOrEven());
        }

        /// <summary>
        /// Retrieves a Queryable of IEnumerable collections that have an odd/even length
        /// and uses fluent syntax.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IQueryable of enumerations</param>
        /// <returns>Filtered IQueryable list of generic collections of odd or even lengths.</returns>
        /// <example>
        /// Filtering of lists based on their odd/even size.
        /// <code>
        /// var test_list = new <List></List><List></List> <int></int>{new List<int></int>{1,3,4,4},
        /// new List<int></int>{43,54,32},
        ///						  new List<int></int>{99,10,403,10,20,10},
        ///						  new List<int></int>{1},
        ///						  new List<int></int>{90,40} }.AsQueryable();
        ///						  
        /// test_list.CollEvenLength(); //returns all lists that have even size.
        /// test_list.CollEvenLength().Take(1); //returns first list of elements that has even size.
        /// test_list.CollEvenLength().Take(2));  //retruns two lists of elements that have even size.
        /// </code>
        /// </example>
        public static IQueryable<IEnumerable<T>> CollEvenLength<T>(this IQueryable<IEnumerable<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            Expression<Func<IEnumerable<T>, bool>> predicate = src => src.OddOrEven();
            return source.Where(predicate);

        }

        /// <summary>
        /// Retrieves a Queryable of string based on IEnumerable of string data types that equal
        /// to a current hamming value of N and input type str2 that are checked against.
        /// </summary>
        /// <param name="str1">The leftmost string part to compare with</param>
        /// <param name="str2">The rightmost string part to compare against</param>
        /// <param name="distCheck">The hamming value to check per string comparison</param>
        /// <returns>Filtered IQueryable list of generic strings with same resulting hamming comparison value.</returns>
        /// <example>
        /// Filtering per string and hamming code comparison part.
        /// <code>
        /// IQueryable<string></string> hamCheckStr = new List<string></string>{"test1","test2","test3","test2","10times49","20times46","times"}.AsQueryable();
        /// var sourceStrings = new List<string></string> { "abc", "def", "xyz" }.AsQueryable();
        ///
        /// sourceStrings.WhereDist("abg",1);
        /// sourceStrings.WhereDist("aee",2);
        /// hamCheckStr.WhereDist("test3",1); // all return the appropriate filtered queryable search results.
        /// </code>
        /// </example>
        public static IQueryable<string> WhereDist(this IQueryable<string> str1, string str2, int distCheck)
        {

            Expression<Func<string, bool>> distFilter = s1 => HammingDist(s1, str2) == distCheck;
            return str1.Where(distFilter);
        }

        /// <summary>
        ///  Retrieves a Queryable listing of the top most frequently occuring objects in the 
        ///  IQueryable input source of object type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IQueryable of generic enumerations</param>
        /// <returns>Filtered IQueryable list of the most frequently occuring objects of types T - Can return multiple types of objects.</returns>
        /// <example>
        /// Retrieval of all most frequently occuring objects in the input IQueryable of objects type.
        /// Multiple return types included in the results.
        /// NOTE: Prespecified class definitions for tests provided at 'code' section.
        /// <code>
        ///
        /// public class Person { public string name = ""; public string surname = ""; public int age = 0; public Person(string n, string sn) { this.name = n; this.surname = sn; } }
        /// public class Animal { public string species = ""; public Animal(string spec) { this.species = spec; } }
        /// public class Dog { public string name = ""; public Dog(string n) { this.name = n; } }
        /// 
        /// var per1 = new Person("john", "six");
        /// var anim3 = new Animal("Reptile");
        /// var anim2 = new Animal("canine");
        /// var anim1 = new Animal("feline");
        /// var dog1 = new Dog("spok");
        /// var per2 = new Person("john", "1");
        /// var dog2 = new Dog("tim");
        /// var per3 = new Person("john", "test");
        /// 
        /// IQueryable<object></object> Freq1 = new List<object></object> { "gtes", 11, 15, 15, anim3, anim2, dog2, dog2, dog2, dog2, dog1, per4, per4, per1, per2, per3, 190, 190, 190, "vdvd", "3489" }.AsQueryable();
        /// 
        ///  Freq1.FreqObjsOccur(); //example for return types of T = Object
        /// </code>
        /// </example>
        public static IQueryable<T> FreqObjsOccur<T>(this IQueryable<T> source)
        {

            Expression<Func<T, bool>> filtElements = el => el != null && el.GetType().IsClass && !(el is string);
            //Retrieve all object groups with the highest count in the collection.
            var classCounts = source?
                .Where(filtElements)
                .GroupBy(item => item.GetType().Name)
                .OrderByDescending(group => group.Count());

            var maxCount = classCounts.FirstOrDefault()?.Count() ?? 0;
            Expression<Func<IGrouping<string, T>, bool>> filtCountOccur = cntGrp => cntGrp.Count() == maxCount;

            //Return groups from highest frequency of object counts (per object type).
            return classCounts.Where(filtCountOccur)?
                          .SelectMany(group => group).AsQueryable();
        }

        /// <summary>
        ///  Retrieves the most frequently occuring object in the 
        ///  IQueryable input source of object type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Input data source of IQueryable of generic enumerations</param>
        /// <returns>The single most frequently-occuring object of type T</returns>
        /// <example>
        /// Retrieval of the most frequently occuring value of either int/string other from IQueryable.
        /// NOTE: Prespecified class definitions for tests provided at 'code' section.
        /// <code>
        /// 
        /// public class Person { public string name = ""; public string surname = ""; public int age = 0; public Person(string n, string sn) { this.name = n; this.surname = sn; } }
        /// public class Animal { public string species = ""; public Animal(string spec) { this.species = spec; } }
        /// public class Dog { public string name = ""; public Dog(string n) { this.name = n; } }
        /// 
        /// var per1 = new Person("john", "six");
        /// var anim3 = new Animal("Reptile");
        /// var anim2 = new Animal("canine");
        /// var anim1 = new Animal("feline");
        /// var dog1 = new Dog("spok");
        /// var per2 = new Person("john", "1");
        /// var dog2 = new Dog("tim");
        /// var per3 = new Person("john", "test");
        /// 
        /// IQueryable<int></int> testFreqOcc = new List<int></int>{1,5,5,6,7,7,7,7,72,2,1,4,6,11,11,11,11,11,88,88,7,34,43,5,88,61,14,23,11,100}.AsQueryable();
        /// IQueryable<string></string> freqSeq2 = new List<string></string> { "gg", "4334", "4334", "4554", "gg", "gg", "gg", "9000" }.AsQueryable();
        /// IQueryable<object></object> Freq1 = new List<object></object> { "gtes", 11, 15, 15, anim3, anim2, dog2, dog2, dog2, dog2, dog1, per4, per4, per1, per2, per3, 190, 190, 190, "vdvd", "3489" }.AsQueryable();
        /// 
        ///  testFreqOcc.FreqOccur(); //example for return types of T = int
        ///  freqSeq2.FreqOccur(); //example for return types of T = string
        ///  Freq1.FreqOccur(); //example for return types of T = Object
        /// </code>
        /// </example>
        public static T FreqOccur<T>(this IQueryable<T> source)
        {
            return source.GroupBy(n => n).OrderByDescending(n => n.Count()).FirstOrDefault().Key;
        }

        /// <summary>
        /// Retrieves a filtered IQueryable list of integer matrices such that their total dimensions equals to
        /// the maximum string length of defaultLen that is prespecified as input parameter.
        /// </summary>
        /// <param name="source">Input data source IQueryable of Large strings of Numerical values</param>
        /// <param name="defaultLen">Maximum used length of the string to be output as an integer matrix
        /// Notes: 
        /// - Defaults to value of zero if none specified as an argument and outputs unfiltered list of transformed strings to matrices.
        /// - When a value above 900 is specified, strings are transformed to 1 dimensional arrays.
        /// </param>
        /// <returns>Filtered IQueryable list of integer matrices based on their string input value counterparts of length = defaultLen</returns>
        /// <example>
        /// Retrieval cases for filtered/transformed input strings that require also data input cleanup.
        /// NOTE: WILL THROW a System.OutofMemoryException for 2-Dim matrix values of sizes 1000x1000. 
        /// Output matrices accomodate string representation of numeric integer values of up to 1M of digits with no issues.
        /// Larger values of above 1M require more detailed testing.
        /// <code>
        /// IQueryable<string></string> BigNumStrs = new List<string></string> { null, "", "0", "1", "0", "99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004411111111111111111111111111111111111111111111111111111555555555555555555555555444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)", "99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)", "999999990000000000000000000000000000000000000000", "ABD34783489BSNCI328932902309239032092390320dnsjiansiNDUsA9802398nfuiwn3e80c9unu2289cn2", "2962728abcs1119__1", null, "89348934988324934438492", "547849823200%$9232", string.Empty, "$7823934222220.00943", @"99999999999\99999.999999\.999" }.AsQueryable();
        /// IQueryable<string></string> BigNumStrsAdded = new List<string></string> { null, "111133354421999999990111110434341113443492-0323--0222999", "999000===493", "84848349349999hsus29229cusudshu0992", "34r4f3f3f3343", }.AsQueryable();
        /// IQueryable<string></string> BigNumStrs2 = new List<string></string> { null, "9999999900", "4389433493", "34r4f3f3f3343", "   83483943fd43f_0f34043f0)/__f34/" }.AsQueryable();
        /// IQueryable<string></string> NullSeq = new List<string></string> { null, null, "", string.Empty, "  334", "b  889 //" }.AsQueryable();
        /// var EmptySetNullChecks = null ?? new List<string></string> { }.AsQueryable(); 
        /// 
        /// BigNumStrs.SelectIntMatrix(); //return all input string-based integer representation as 2-dimensional arrays in an IQueryable.
        /// BigNumStrsAdded.SelectIntMatrix(2232); // return the input string-based integer representation of specified length as a 1-dimensional array.
        /// BigNumStrsAdded.SelectIntMatrix(237); //return the input string-based integer representation of specified length as a 2-dimensional array.
        /// EmptySetNullChecks .SelectIntMatrix(); //Result N/A, will throw an exception if left with no null default.
        /// </code>
        /// </example>
        public static IQueryable<int[,]> SelectIntMatrix(this IQueryable<string> source, int defaultLen = 0)
        {

            Expression<Func<string, string>> changeStr = strEl => Regex.Replace(string.IsNullOrEmpty(strEl) ? "0" : strEl, @"^\D$", "",RegexOptions.None,TimeSpan.FromSeconds(25));
            //preset filter for default values equality
            Func<int, int> toUnsigned = (len) => len < 0 ? (-len) : len;
            int limitInt = toUnsigned(defaultLen);

            Expression<Func<string, bool>> defaultEquality = str => str.Length == limitInt;


            IQueryable<int[,]> resultMatrix = Enumerable.Empty<int[,]>().AsQueryable();

            if (limitInt <= 900)
            {
                Expression<Func<string, int[,]>> transfMap = str => ConvertTo2Dim(new[] { str }.AsQueryable(),

                (int)Char.GetNumericValue(str.Length.ToString()[0]),
                (str.Length) / (int)Char.GetNumericValue(str.Length.ToString()[0])
                );

                //default filtering part, returns full set of values as 2dim array nxm
                var executedSource = (limitInt == 0) ? source.Select(changeStr) : source.Select(changeStr).Where(defaultEquality);
                resultMatrix = executedSource.Select(transfMap.Compile()).AsQueryable();
            }
            else
            {
                //alternative filter and return 1 row in a 2dim aray 1xm
                Expression<Func<string, int[,]>> transfMapSingleRow = str => new int[1, str.Length].Cast<int>()
                        .Select((_, index) => char.IsDigit(str[index]) ? (int)char.GetNumericValue(str[index]) : 0)
                        .ToArray()
                        .To2DArray(1, str.Length);

                var executedSource = source.Select(changeStr).Where(defaultEquality);
                var transfSource = executedSource.Select(transfMapSingleRow.Compile());
                resultMatrix = transfSource.AsQueryable();
            }

            //returned transformed info part.
            return resultMatrix;
        }

        /// <summary>
        /// Retrieves a filtered IQueryable list of integer matrices,
        /// based on their corresponding filtering condition of the anonymous function as parameter.
        /// </summary>
        /// <param name="source">Input data source IQueryable of Large strings of Numerical values</param>
        /// <param name="conditions">Anonymous function used for conditional predicate selections for retrieving IQueryable of transformed 2-dim matrices</param>
        /// <returns>Filtered IQueryable list of integer matrices transformed based on the conditional predicate of the anonymous parameter function as an input</returns>
        /// <example>
        /// Retrieval cases for filtered/transformed input strings that require also data input cleanup.
        /// NOTE: WILL THROW a System.OutofMemoryException for 2-Dim matrix values of sizes 1000x1000. 
        /// Output matrices accomodate string representation of numeric integer values of up to 1M of digits with no issues.
        /// Larger values of above 1M require more detailed testing.
        /// <code>
        /// IQueryable<string></string> BigNumStrs = new List<string></string> { null, "", "0", "1", "0", "99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004411111111111111111111111111111111111111111111111111111555555555555555555555555444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)", "99999999004444444444444444444444444444444444444444444499999999000007777777777777777777777777777777777000000000000000009999999900000iiiiiiiiiii20202282828991111000009999888888888888888888888888888888888888888888888888888888888uu929192919uuuu00000000000013131313444(*)444_(*)", "999999990000000000000000000000000000000000000000", "ABD34783489BSNCI328932902309239032092390320dnsjiansiNDUsA9802398nfuiwn3e80c9unu2289cn2", "2962728abcs1119__1", null, "89348934988324934438492", "547849823200%$9232", string.Empty, "$7823934222220.00943", @"99999999999\99999.999999\.999" }.AsQueryable();
        /// IQueryable<string></string> BigNumStrsAdded = new List<string></string> { null, "111133354421999999990111110434341113443492-0323--0222999", "999000===493", "84848349349999hsus29229cusudshu0992", "34r4f3f3f3343", }.AsQueryable();
        /// IQueryable<string></string> BigNumStrs2 = new List<string></string> { null, "9999999900", "4389433493", "34r4f3f3f3343", "   83483943fd43f_0f34043f0)/__f34/" }.AsQueryable();
        /// IQueryable<string></string> NullSeq = new List<string></string> { null, null, "", string.Empty, "  334", "b  889 //" }.AsQueryable();
        /// var EmptySetNullChecks = null ?? new List<string></string> { }.AsQueryable(); 
        /// 
        /// BigNumStrs.SelectIntMatrix(n => n.Length > 3422 || n.Length != 2232); // will return all that match string size of above 3422 as a 1-dim array and everything that is of string size 2232 as a 2-dim one.
        /// BigNumStrs.SelectIntMatrix(); // wille return all input string-based integer representation as 2-dim arrays in an IQueryable.
        /// BigNumStrsAdded.SelectIntMatrix(n => n.Length == 900); // will return all predicate conditional matches as a 1-dim array if not matched returns  a 2-dim one.
        /// BigNumStrsAdded.SelectIntMatrix(n => n.Length != 9000); //will return all predicate conditional matches as a 1-dim array if not matched returns  a 2-dim one.
        /// 
        /// EmptySetNullChecks .SelectIntMatrix() //Result N/A, will throw an exception if left with no null default.
        /// </code>
        /// </example>
        public static IQueryable<int[,]> SelectIntMatrix(this IQueryable<string> source, params Func<string, bool>[] conditions)
        {

            //add projected part directly to 2-dim if no arguments.. 
            if (conditions.Length == 0)
            {
                //checks for nulls etc prior to cleaning processed data..
                Expression<Func<string, string>> changeStr = strEl => Regex.Replace(string.IsNullOrEmpty(strEl) ? "0" : strEl, @"^\D$", "",RegexOptions.None,TimeSpan.FromSeconds(25));
                Expression<Func<string, int[,]>> transfMap = str => ConvertTo2Dim(new[] { str }.AsQueryable(),

                (int)Char.GetNumericValue(str.Length.ToString()[0]),
                (str.Length) / (int)Char.GetNumericValue(str.Length.ToString()[0])
                );


                var executedSource = source.Select(changeStr);
                var resultArrays = executedSource.Select(transfMap.Compile());

                return resultArrays.AsQueryable();
            }
            else
            { //project the selected items per length into 1-dim counterpart..
                Expression<Func<string, string>> changeStr = strEl => Regex.Replace(string.IsNullOrEmpty(strEl) ? "0" : strEl, @"^\D$", "",RegexOptions.None,TimeSpan.FromSeconds(25));

                Expression<Func<string, int[,]>> transfMap = str => (conditions.Any(condition => condition(str))) ?
                new int[1, str.Length]
                .Cast<int>()
                .Select((_, index) => char.IsDigit(str[index]) ? (int)char.GetNumericValue(str[index]) : 0)
                .ToArray()
                .To2DArray(1, str.Length)
                : ConvertTo2Dim(new[] { str }.AsQueryable(), (int)Char.GetNumericValue(str.Length.ToString()[0]), (str.Length) / (int)Char.GetNumericValue(str.Length.ToString()[0]));

                var executedSource = source.Select(changeStr);
                var resultArrays = executedSource.Select(transfMap.Compile());
                return resultArrays.AsQueryable();
            }
        }

        /// <summary>
        /// Transforms 1-dimensional array input to it's 2-dimensional counterpart
        /// based on prespecified parameters rows/columns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flatArray">Input 1-dimensional array of N length.</param>
        /// <param name="rows">Number of rows N to be exported.</param>
        /// <param name="columns">Number of columns N to be exported.</param>
        /// <returns>A 2-dimensional array of the specified data type that is of the prespecified rows-columns size.</returns>
        private static T[,] To2DArray<T>(this T[] flatArray, int rows, int columns)
        {
            var array2D = new T[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array2D[i, j] = flatArray[i * columns + j];
                }
            }
            return array2D;
        }

        /// <summary>
        /// Transforms from an IQueryable of strings to it's 2-dimensional matrix of integers counterpart
        /// based on prespecified parameters rows/columns.
        /// </summary>
        /// <param name="source">Input data source of IQueryable of enumerations</param>
        /// <param name="rows">Number of rows N to be exported.</param>
        /// <param name="columns">Number of columns N to be exported.</param>
        /// <returns>A 2-dimensional array of integer data type that is of the prespecified rows-columns size.</returns>
        private static int[,] ConvertTo2Dim(this IQueryable<string> source, int rows, int columns)
        {

            // Return nothing from nothing for efficient exec..
            if (source == null) { return new int[0, 0] { }; }
            else
            {
                // Add info to an int 2dim array..
                var filteredDigits = source.SelectMany(str => str.Where(char.IsDigit).Select(char.GetNumericValue)).ToArray();
                var result = new int[rows, columns];
                int index = 0;

                Parallel.For(0, rows, i => {
                    for (int j = 0; j < columns; j++)
                    {
                        if (index < filteredDigits.Length)
                        {
                            result[i, j] = (index < filteredDigits.Length) ? (int)filteredDigits[index] : 0;
                            index++;
                        }
                        else
                        {
                            result[i, j] = 0; // Empty elements default to zero in case more indexes are allocated.
                        }
                    }
                });
                return result;
            }
        }

        /// <summary>
        /// Source Implementation for custom "Select()"
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Select<TSource, T>(
           this IQueryable<TSource> source,
           Expression<Func<TSource, T>> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Provider.CreateQuery<TSource>(
             Expression.Call(
             typeof(Queryable),
             "Select",
             new[] { typeof(TSource), typeof(T) },
             source.Expression,
             Expression.Quote(predicate)
             )
           );
        }

        /// <summary>
        /// Source Implementation for custom "Where()"
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Where<TSource>(
          this IQueryable<TSource> source,
          Expression<Func<TSource, bool>> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Provider.CreateQuery<TSource>(
                Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new[] { typeof(TSource) },
                    source.Expression,
                    Expression.Quote(predicate)
                )
            );
        }

        /// <summary>
        /// Method for filtering data by their corresponding data type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The type of data to be processed as numeric.</param>
        /// <returns>True or False depending on whether the input data type is of any number.</returns>
        private static bool IsNumericType<T>(T value)
        {
            Type type = value.GetType() ?? typeof(object);
            return type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal);
        }

        /// <summary>
        /// Method for filtering enumerations based on odd/even length of an IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements">Input data type of IEnumerable sequence of elements</param>
        /// <returns>True or False depending on whether the data length of input data sequence is of even length or not.</returns>
        private static bool OddOrEven<T>(this IEnumerable<T> elements)
        {
            return ((elements.Count() % 2) == 0);
        }

        /// <summary>
        /// Method for returing hamming-distance between two input strings (left and right).
        /// NOTE: Input strings must be of equal size for relevant comparison.
        /// </summary>
        /// <param name="leftStr">The leftmost string part to compare with</param>
        /// <param name="rightStr">The rightmost string part to compare against</param>
        /// <returns>The total bit count difference between leftmost and rightmost input string parts
        /// for equal length of strings, maximum integer value when length of input strings is not equal.
        /// </returns>
        private static int HammingDist(this string leftStr, string rightStr)
        {
            return (leftStr.Length != rightStr.Length) ? Int32.MaxValue
                                                        : leftStr.Zip(rightStr, (l, r) => l != r ? 1 : 0).Sum();
        }


    }
}