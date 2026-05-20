//#define MAIN

#if MAIN
using System.Xml.Serialization;

namespace C_
{
    public class Point 
    {
        public int x, y;
        public Point(int x, int y) 
        {
            this.x = x; this.y = y;
        }
        public Point() { }
    }

    public class Company
    {
        // Уровень 1
        public string Name;
        public int EstablishedYear;
        public string[] CoreTechnologies; // Массив простых типов (Уровень 1)
        public Department[] Departments;
    }

    public class Department
    {
        // Уровень 2
        public string DepartmentName;
        public string HeadManager;
        public int[] ProjectBudgets;     // Массив простых типов (Уровень 2)
        public Team[] Teams;             // Массив объектов (Уровень 2)
    }

    public class Team
    {
        // Уровень 3
        public string TeamName;
        public bool IsRemote;
        public string[] ActiveSprints;   // Массив простых типов (Уровень 3)
        public Employee[] Employees;     // Массив объектов (Уровень 3)
    }

    public class Employee
    {
        // Уровень 4
        public string FullName;
        public string Role;
        public double Salary;
        public string[] Skills;
    }


    internal class main_5
    {
        static void Main()
        {
            {
                Point point = new Point(-99, 100);
                //Сериализация - приведение к строке
                //Десериализация - из строки к объекту

                XmlSerializer xmlserializer = new XmlSerializer(typeof(Point));
                //Serialize - запись объекта
                /*
                 *Stream - поток куда записать XML
                 *Object - объект для записи
                 */
                using (FileStream fs = new FileStream("point.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        xmlserializer.Serialize(sw, point);
                    }
                }
            }

            {
                Company company = GetCompany();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));

                using (FileStream fileStream = new FileStream("company.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        xmlSerializer.Serialize(streamWriter, company);
                    }
                }
            }

            {
                Random rand = new Random();
                using (FileStream fileStream = new FileStream("data_1.txt", FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        // Цикл для генерации и записи 5 чисел
                        for (int i = 0; i < 5; i++)
                        {
                            // rand.Next(100) генерирует числа от 0 до 99
                            int zeroToNintyNine = rand.Next(100);
                            streamWriter.WriteLine(zeroToNintyNine);
                        }
                    }
                }
            }
        }
        static Company GetCompany()
        {
            return new Company
            {
                // Уровень 1
                Name = "CyberNova Games",
                EstablishedYear = 2021,
                CoreTechnologies = new string[] { "C#", "Unity", "Unreal Engine", "Blender" },
                Departments = new Department[]
                {
                    new Department
                    {
                        // Уровень 2
                        DepartmentName = "Game Development",
                        HeadManager = "Alex Mercer",
                        ProjectBudgets = new int[] { 450000, 1200000, 85000 },
                        Teams = new Team[]
                        {
                            new Team
                            {
                                // Уровень 3
                                TeamName = "Team Alpha (Core Gameplay)",
                                IsRemote = true,
                                ActiveSprints = new string[] { "SP-102_Physics", "SP-103_UI_Refactor" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        // Уровень 4
                                        FullName = "Dmitry Petrov",
                                        Role = "Lead Gameplay Programmer",
                                        Salary = 115000.00,
                                        Skills = new string[] { "C#", "Vector Math", "Optimization", "Git" }
                                    },
                                    new Employee
                                    {
                                        FullName = "Elena Rostova",
                                        Role = "Senior 3D Animator",
                                        Salary = 95000.00,
                                        Skills = new string[] { "Maya", "Rigging", "Motion Capture" }
                                    }
                                }
                            },
                            new Team
                            {
                                TeamName = "Team Beta (Multiplayer)",
                                IsRemote = false,
                                ActiveSprints = new string[] { "SP-089_Matchmaking" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        FullName = "Marcus Vance",
                                        Role = "Network Engineer",
                                        Salary = 130000.50,
                                        Skills = new string[] { "C++", "Photon", "gRPC", "Docker" }
                                    }
                                }
                            }
                        }
                    },
                    new Department
                    {
                        DepartmentName = "Marketing & PR",
                        HeadManager = "Sarah Connor",
                        ProjectBudgets = new int[] { 30000, 15000 },
                        Teams = new Team[]
                        {
                            new Team
                            {
                                TeamName = "SMM & Community",
                                IsRemote = true,
                                ActiveSprints = new string[] { "SP-201_Trailer_Release" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        FullName = "Anna Lee",
                                        Role = "Community Manager",
                                        Salary = 60000.00,
                                        Skills = new string[] { "Copywriting", "Discord API", "Public Relations" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

        }
    }
}
#endif //MAIN