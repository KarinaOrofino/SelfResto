using System.Text;
using Framework.Helpers;
using System.Net.Sockets;
using System.Threading.Tasks;
using KO.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KO.Servicios
{
    public class ServicioSocket : IServicioSocket
    {        
        protected int SERVICE_PORT;
        protected string SERVICE_IP;

        protected TcpClient _tcpClient;
        protected NetworkStream _stream;

        public ServicioSocket(IConfiguration configuration)
        {
            SERVICE_IP = configuration["ServiceSocket.Ip"];
            SERVICE_PORT = int.Parse(configuration["ServiceSocket.Port"]);
        }
               
        public async Task<string> SendAndWaitForResponse(string message)
        {
            if(_tcpClient == null){
                _tcpClient = new TcpClient(SERVICE_IP, SERVICE_PORT);
                _stream = _tcpClient.GetStream();
            }

            if (!_tcpClient.Connected){
                _tcpClient.Connect(SERVICE_IP, SERVICE_PORT);
                _stream = _tcpClient.GetStream();
            }

            if(!message.EndsWith("<EOF>"))
                message += "<EOF>";

            await SendMessage(_stream, message);
            string serverResponse = await ReadMessage(_stream);

            return serverResponse;
        }

        private async Task SendMessage(NetworkStream stream, string message)
        {
            string encryptedMessage = EncryptionHelper.Encrypt(message);
            byte[] bytes = Encoding.UTF8.GetBytes(encryptedMessage);
            await stream.WriteAsync(bytes);
            await stream.FlushAsync();
        }

        async Task<string> ReadMessage(NetworkStream stream)
        {
            // El final del mensaje está marcado con "<EOF>".
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Evaluar por EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            var decryptedMessage = EncryptionHelper.Decrypt(messageData.ToString());

            return decryptedMessage.Remove(decryptedMessage.Length - 5, 5).ToString();
        }
    }
}
