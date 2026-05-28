//#define H_W_4
using Loger;
using System.Xml;
using System.Xml.Xsl;
using System.Text;
using HtmlAgilityPack;

#if H_W_4
class Program
{
    static void Main()
    {
        {
            // Путь к INI-файлу и лог-файлу
            string configPath = "config.ini";
            string logPath = "app.log";

            // Создаём логгер
            Logger logger = new Logger(configPath, logPath);

            // Записываем логи
            logger.Log(Logger.LogType.Info, "test testy", "Программа запущена.");
            logger.Log(Logger.LogType.Error, "test testy", "Ошибка при загрузке данных.");
            logger.Log(Logger.LogType.Warning, "test testy", "Низкий уровень памяти.");

            Console.WriteLine("Логи записаны в файл app.log");
            Console.WriteLine();
        }

        {
            // Задание 1: Экспорт курса доллара в output.xml
            Task1();

            // Задание 2: Создание XML с заказами
            Task2();

            // Задание 3: XSLT-преобразование XML в HTML
            Task3();

            // Задание 4: Чтение XML
            Task4();
        }
    }
    static void Task1()
    {
        try
        {
            // 1. Сохраняем HTML-страницу в файл
            string htmlContent = new HtmlWeb().Load("http://finance.i.ua/").DocumentNode.OuterHtml;
            File.WriteAllText("finance_page.html", htmlContent, Encoding.UTF8);

            // 2. Парсим HTML
            HtmlDocument doc = new HtmlDocument();
            doc.Load("finance_page.html");

            // 3. Ищем таблицу с курсами
            var rows = doc.DocumentNode.SelectNodes("//table[contains(@class, 'currency')]//tr");
            if (rows == null || rows.Count < 2)
            {
                Console.WriteLine("Таблица курсов не найдена.");
                return;
            }

            // 4. Создаём XML
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("Banks");
            xml.AppendChild(root);

            // 5. Заполняем XML данными (пропускаем заголовок)
            foreach (var row in rows.Skip(1))
            {
                var cells = row.SelectNodes(".//td");
                if (cells == null || cells.Count < 3) continue;

                // Проверяем, что ячеек достаточно
                string bankName = cells[0].InnerText.Trim();
                string buyRate = cells[1].InnerText.Trim();
                string sellRate = cells[2].InnerText.Trim();

                if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(buyRate) || string.IsNullOrEmpty(sellRate))
                    continue;

                XmlElement bank = xml.CreateElement("Bank");
                bank.SetAttribute("Name", bankName);

                XmlElement buy = xml.CreateElement("BuyRate");
                buy.InnerText = buyRate;
                bank.AppendChild(buy);

                XmlElement sell = xml.CreateElement("SellRate");
                sell.InnerText = sellRate;
                bank.AppendChild(sell);

                root.AppendChild(bank);
            }

            xml.Save("output.xml");
            Console.WriteLine("Задание 1: Данные сохранены в output.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в Задании 1: {ex.Message}");
        }
    }
    static void Task2()
    {
        try
        {
            using (XmlTextWriter writer = new XmlTextWriter("orders.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Orders");

                // Заказ 1
                WriteOrder(writer, "1", "Иван Иванов", new[] {
                (Name: "Ноутбук", Price: "1000.00", Quantity: "1"),
                (Name: "Мышь", Price: "20.00", Quantity: "2")
            });

                // Заказ 2
                WriteOrder(writer, "2", "Петр Петров", new[] {
                (Name: "Клавиатура", Price: "50.00", Quantity: "1")
            });

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            Console.WriteLine("Задание 2: Данные сохранены в orders.xml");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в Задании 2: {ex.Message}");
        }
    }

    // Вспомогательный метод для записи заказа
    static void WriteOrder(XmlTextWriter writer, string id, string customer, (string Name, string Price, string Quantity)[] items)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(customer) || items == null)
            throw new ArgumentException("Некорректные данные заказа.");

        writer.WriteStartElement("Order");
        writer.WriteAttributeString("ID", id);
        writer.WriteAttributeString("Date", DateTime.Now.ToString("yyyy-MM-dd"));

        writer.WriteStartElement("Customer");
        writer.WriteString(customer);
        writer.WriteEndElement();

        writer.WriteStartElement("Items");
        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Price) || string.IsNullOrEmpty(item.Quantity))
                continue;

            writer.WriteStartElement("Item");
            writer.WriteAttributeString("Name", item.Name);
            writer.WriteAttributeString("Price", item.Price);
            writer.WriteAttributeString("Quantity", item.Quantity);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.WriteEndElement();
    }
    static void Task3()
    {
        try
        {
            // Проверяем, существует ли orders.xml
            if (!File.Exists("orders.xml"))
            {
                Console.WriteLine("Файл orders.xml не найден.");
                return;
            }

            // Создаём XSLT-файл
            string xslt = @"<?xml version='1.0'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
    <xsl:template match='/'>
        <html>
            <head>
                <meta charset='UTF-8'/>
                <title>Заказы</title>
            </head>
            <body>
                <h1>Заказы</h1>
                <table border='1'>
                    <tr>
                        <th>ID</th>
                        <th>Дата</th>
                        <th>Клиент</th>
                        <th>Товары</th>
                    </tr>
                    <xsl:for-each select='Orders/Order'>
                        <tr>
                            <td><xsl:value-of select='@ID'/></td>
                            <td><xsl:value-of select='@Date'/></td>
                            <td><xsl:value-of select='Customer'/></td>
                            <td>
                                <xsl:for-each select='Items/Item'>
                                    <span><xsl:value-of select='@Name'/> - <xsl:value-of select='@Price'/>$ (x<xsl:value-of select='@Quantity'/>)<br/></span>
                                </xsl:for-each>
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>";

            File.WriteAllText("orders.xslt", xslt);

            // Применяем XSLT
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load("orders.xslt");
            transform.Transform("orders.xml", "orders.html");

            Console.WriteLine("Задание 3: HTML сгенерирован в orders.html");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в Задании 3: {ex.Message}");
        }
    }
    static void Task4()
    {
        try
        {
            Console.WriteLine("\n--- Чтение через XmlDocument ---");
            XmlDocument doc = new XmlDocument();
            doc.Load("orders.xml");

            foreach (XmlNode order in doc.SelectNodes("/Orders/Order"))
            {
                string id = order.Attributes?["ID"]?.Value ?? "N/A";
                string date = order.Attributes?["Date"]?.Value ?? "N/A";
                string customer = order["Customer"]?.InnerText ?? "N/A";

                Console.WriteLine($"Заказ #{id} от {date}, Клиент: {customer}");
                foreach (XmlNode item in order.SelectNodes("Items/Item"))
                {
                    string name = item.Attributes?["Name"]?.Value ?? "N/A";
                    string price = item.Attributes?["Price"]?.Value ?? "0";
                    string quantity = item.Attributes?["Quantity"]?.Value ?? "0";
                    Console.WriteLine($"- {name}, Цена: {price}$, Кол-во: {quantity}");
                }
            }

            Console.WriteLine("\n--- Чтение через XmlTextReader ---");
            using (XmlReader reader = XmlReader.Create("orders.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Order")
                    {
                        string id = reader.GetAttribute("ID") ?? "N/A";
                        string date = reader.GetAttribute("Date") ?? "N/A";
                        string customer = reader.ReadElementContentAsString("Customer", "");

                        Console.WriteLine($"Заказ #{id} от {date}, Клиент: {customer}");

                        // Чтение товаров
                        if (reader.ReadToFollowing("Items"))
                        {
                            int depth = reader.Depth;
                            while (reader.Read() && reader.Depth > depth)
                            {
                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Item")
                                {
                                    string name = reader.GetAttribute("Name") ?? "N/A";
                                    string price = reader.GetAttribute("Price") ?? "0";
                                    string quantity = reader.GetAttribute("Quantity") ?? "0";
                                    Console.WriteLine($"- {name}, Цена: {price}$, Кол-во: {quantity}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в Задании 4: {ex.Message}");
        }
    }
}
#endif //H_W_4