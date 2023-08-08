using RabbitMQ.Client;
using System.Text;

namespace WeatherAPI.Clients
{
    internal class RpcClient /*: IDisposable*/
    {
        //private const string _queueName = "WeatherRpcRequestQueue";

        //// Основной интерфейс для соединения AMQP.
        //// Экземпляры IConnection используются для создания новых сеансов/каналов.
        //private RabbitMQ.Client.IConnection _connection;
        //// Общая модель AMQP, объединяющая функциональные возможности
        //private RabbitMQ.Client.IModel _channel;
        //private bool _isDisposed;

        //public void InitializeAndRun()
        //{
        //    var factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost" };

        //    // IConnection абстрагирует соединение сокета и заботится о согласовании версии протокола, аутентификации и так далее.
        //    // Здесь мы подключаемся к узлу RabbitMQ на локальной машине.
        //    _connection = factory.CreateConnection();

        //    // Затем мы создаем канал, в котором находится большая часть API для выполнения задач.
        //    _channel = _connection.CreateModel();

        //    // Чтобы отправить, мы должны объявить очередь для отправки;
        //    // то мы можем опубликовать сообщение в очередь.
        //    _channel.QueueDeclare(
        //        queue: _queueName,
        //        durable: false,
        //        exclusive: false,
        //        autoDelete: false,
        //        arguments: null);

        //    var message = "Hello World!";
        //    var body = Encoding.UTF8.GetBytes(message);

        //    _channel.BasicPublish(
        //        exchange: string.Empty,
        //        routingKey: "hello",
        //        basicProperties: null,
        //        body: body);

        //    Console.WriteLine($"[x] Sent {message}");

        //    // Настраивает параметры QoS базового класса контента.
        //    _channel.BasicQos(0, 1, false);

        //    var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(_channel);
        //    _channel.BasicPublish(queue: _queueName, autoAck: false, consumer: consumer);
        //}
    }
}
