/*using Exercise2;

Student student = new Student()
{
    ID = 1,
    Imie = "Jan",
    Nazwisko = "Kowalski",
    Studies = new Studies
    {
        Name = "Informatyka",
        Mode = "Dzienne"
    }
};

var studentWithNULL = new Student()
{
    ID = 1,
    Imie = "",
    Nazwisko = null
};

string[]
ArrayList<T>
Set<T>
HashMap<K, V>

Kolekcje
List<string> list = new List<string>();
list.Add("Tesst");
if (list.Count > 0)
    list.Remove("Tesst");
HashSet<string> set = new HashSet<string>();
set.Add("Test");
if (set.Count > 0)
    list.Remove("Test");
Dictionary<string, string> dict = new Dictionary<string, string>();
dict.Add("Test", "Test");
if (dict.Count > 0)
    dict.Remove("Test");

void MetodaSync()
{
    var httpClient = new HttpClient();
}
async Task MetodaAsync()
{
    var httpClient = new HttpClient();
    await httpClient.GetAsync("https://pja.edu.pl");
    await httpClient.GetAsync("https://pja.edu.pl");
    await httpClient.GetAsync("https://pja.edu.pl");
}
async Task MetodaAsync2()
{
    var httpClient = new HttpClient();
    Task<HttpResponseMessage> r1 = httpClient.GetAsync("https://pja.edu.pl");
    Task<HttpResponseMessage> r2 = httpClient.GetAsync("https://pja.edu.pl");
    Task<HttpResponseMessage> r3 = httpClient.GetAsync("https://pja.edu.pl");

    await Task.WhenAll(r1, r2, r3);
}

await MetodaAsync();
await MetodaAsync2();


void Metoda()
{

}

int MetodaWithReturn()
{
    return 1;
}*/

using Exercise2;
using System.Text.Json;

if (args.Length != 4) throw new ArgumentOutOfRangeException("Too many/too few arguments");
if (!File.Exists(args[0])) throw new FileNotFoundException("File not found");
if (!Directory.Exists(args[1])) throw new DirectoryNotFoundException("Directory not found");
if (!File.Exists(args[2])) throw new FileNotFoundException("Logs file not founds");
var pathSource = args[0];
var fileType = args[3];
if (fileType != "json") throw new InvalidOperationException("App can only use json");
var pathForExport = args[1] + "\\uczelnia." + fileType;
var pathForLogs = args[2];


var students = new HashSet<Student>(new StudentComparer());
var studyNames = new List<string>();
var numberOfStudents = new List<int>();
var wrongStudents = new HashSet<string>();
var activeStudies = new List<ActiveStudies>();

var studentLines = await File.ReadAllLinesAsync(pathSource);
foreach(string studentLine in studentLines)
{
   
    var data = studentLine.Split(',');

    //sprawdzenie czy wiersz ma odp lb kolumn
    if (data.Length < 9)
    {
        wrongStudents.Add("Wiersz nie posiada odpowiedniej ilości kolumn: " + studentLine);
        continue;
    }

    //sprawdzenie czy nie ma pustych wartosci
    if (data.Contains(""))
    {
        wrongStudents.Add("Wiersz nie może posiadać pustych kolumn: " + studentLine);
        continue;
    }
    var date = data[5].Split('-');
    var student = new Student
    {
        indexNumber = "s" + data[4],
        fname = data[0],
        lname = data[1],
        birthdate = date[2] + '.' + date[1] + '.' + date[0],
        email = data[6],
        mothersName = data[7],
        fathersName = data[8],
        studies = new Studies
        {
            name = data[2],
            mode = data[3]
        }
        
    };
    

    Console.WriteLine(studentLine);
    //duplikaty
    if (students.Contains(student))
    {
        wrongStudents.Add("Duplikat: " + studentLine);
        continue;
    }
    else
    {
        students.Add(student);
    }
    if (studyNames.Contains(student.studies.name))
    {
        var index = studyNames.IndexOf(student.studies.name);
        numberOfStudents[index]++;
    }
    else
    {
        studyNames.Add(student.studies.name);
        numberOfStudents.Add(1);
    }
}

for (int i = 0; i < studyNames.Count; i++)
{
    activeStudies.Add(new ActiveStudies
    {
        name = studyNames[i],
        numberOfStudents = numberOfStudents[i]
    });
}

var uczelnia = new Uczelnia
{
    createdAt = DateTime.Now.ToString("dd.MM.yyyy"),
    author = "Piotr Nowicki",
    students = students,
    activeStudies = activeStudies
};



JsonSerializerOptions serializerOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

var json = JsonSerializer.Serialize(new { Uczelnia = uczelnia }, serializerOptions);
   


//var json2 = JsonSerializer.Serialize(new { Uczelnia = new { Students = students } }, serializerOptions);

await File.WriteAllTextAsync(pathForExport, json);
await File.WriteAllLinesAsync(pathForLogs, wrongStudents);