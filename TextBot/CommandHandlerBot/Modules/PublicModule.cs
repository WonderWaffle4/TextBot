using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using _02_commands_framework.Services;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Collections.Generic;

namespace _02_commands_framework.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us

        [Command("layout")]
        public async Task Layout(params string[] text)
        {
            if (text.Length == 0)
                await ReplyAsync("__**Ошибка**__ отсутствует переводимый текст.");
            else
            {
                Dictionary<char, char> translate1 = new Dictionary<char, char>
                {
                    {'a', 'ф'},
                    {'A', 'Ф'},
                    {'b', 'и'},
                    {'B', 'И'},
                    {'c', 'с'},
                    {'C', 'С'},
                    {'d', 'в'},
                    {'D', 'В'},
                    {'e', 'у'},
                    {'E', 'У'},
                    {'f', 'а'},
                    {'F', 'А'},
                    {'g', 'п'},
                    {'G', 'П'},
                    {'h', 'р'},
                    {'H', 'Р'},
                    {'i', 'ш'},
                    {'I', 'Ш'},
                    {'j', 'о'},
                    {'J', 'О'},
                    {'k', 'л'},
                    {'K', 'Л'},
                    {'l', 'д'},
                    {'L', 'Д'},
                    {'m', 'ь'},
                    {'M', 'Ь'},
                    {'n', 'т'},
                    {'N', 'Т'},
                    {'o', 'щ'},
                    {'O', 'Щ'},
                    {'p', 'з'},
                    {'P', 'З'},
                    {'q', 'й'},
                    {'Q', 'Й'},
                    {'r', 'к'},
                    {'R', 'К'},
                    {'s', 'ы'},
                    {'S', 'Ы'},
                    {'t', 'е'},
                    {'T', 'Е'},
                    {'u', 'г'},
                    {'U', 'Г'},
                    {'v', 'м'},
                    {'V', 'М'},
                    {'w', 'ц'},
                    {'W', 'Ц'},
                    {'x', 'ч'},
                    {'X', 'Ч'},
                    {'y', 'н'},
                    {'Y', 'Н'},
                    {'z', 'я'},
                    {'Z', 'Я'},
                    {'[', 'х'},
                    {'{', 'Х'},
                    {']', 'ъ'},
                    {'}', 'Ъ'},
                    {';', 'ж'},
                    {':', 'Ж'},
                    {'\'', 'э'},
                    {'\"', 'Э'},
                    {',', 'б'},
                    {'<', 'Б'},
                    {'.', 'ю'},
                    {'>', 'Ю'},
                    {'&', '?'},
                    {'#', '№'},
                    {'@', '\"'}
                };
                Dictionary<char, char> translate2 = new Dictionary<char, char>
                {
                    {'ф', 'a'},
                    {'Ф', 'A'},
                    {'и', 'b'},
                    {'И', 'B'},
                    {'с', 'c'},
                    {'С', 'C'},
                    {'в', 'd'},
                    {'В', 'D'},
                    {'у', 'e'},
                    {'У', 'E'},
                    {'а', 'f'},
                    {'А', 'F'},
                    {'п', 'g'},
                    {'П', 'G'},
                    {'р', 'h'},
                    {'Р', 'H'},
                    {'ш', 'i'},
                    {'Ш', 'I'},
                    {'о', 'j'},
                    {'О', 'J'},
                    {'л', 'k'},
                    {'Л', 'K'},
                    {'д', 'l'},
                    {'Д', 'L'},
                    {'ь', 'm'},
                    {'Ь', 'M'},
                    {'т', 'n'},
                    {'Т', 'N'},
                    {'щ', 'o'},
                    {'Щ', 'O'},
                    {'з', 'p'},
                    {'З', 'P'},
                    {'й', 'q'},
                    {'Й', 'Q'},
                    {'к', 'r'},
                    {'К', 'R'},
                    {'ы', 's'},
                    {'Ы', 'S'},
                    {'е', 't'},
                    {'Е', 'T'},
                    {'г', 'u'},
                    {'Г', 'U'},
                    {'м', 'v'},
                    {'М', 'V'},
                    {'ц', 'w'},
                    {'Ц', 'W'},
                    {'ч', 'x'},
                    {'Ч', 'X'},
                    {'н', 'y'},
                    {'Н', 'Y'},
                    {'я', 'z'},
                    {'Я', 'Z'},
                    {'х', '['},
                    {'Х', '{'},
                    {'ъ', ']'},
                    {'Ъ', '}'},
                    {'ж', ';'},
                    {'Ж', ':'},
                    {'э', '\''},
                    {'Э', '\"'},
                    {'б', ','},
                    {'Б', '<'},
                    {'ю', '.'},
                    {'Ю', '>'},
                    {'?', '&'},
                    {'№', '#'},
                    {'\"', '@'}
                };
                string strText = string.Join(" ", text);
                string newstr = "";
                char symbol;
                foreach(char i in strText)
                {
                    if (translate1.TryGetValue(i, out symbol))
                        newstr += symbol;
                    else if (translate2.TryGetValue(i, out symbol))
                        newstr += symbol;
                    else
                        newstr += i;
                }
                await ReplyAsync(newstr);
            }
        }
        [Command ("sendDelay")]
        public async Task SendDelay(string seconds, params string[] text)
        {
            ulong time;
            string message;
            if (seconds == null || !ulong.TryParse(seconds, out time))
                await ReplyAsync("__**Ошибка**__ время не задано или является отрицательным число.");
            else if (time < 0)
                await ReplyAsync("__**Ошибка**__ временной промежуток введён некорректно.");
            else if (text.Length == 0)
                await ReplyAsync("__**Ошибка**__ отсутствует текст.");
            else
            {
                message = string.Join(" ", text);
                await Context.Message.DeleteAsync();
                await Task.Delay(TimeSpan.FromSeconds(time));
                await ReplyAsync(message);
            }
        }
        [Command ("deleteDelay")]
        public async Task DeleteDelay(string seconds)
        {
            ulong time;
            SocketMessage message = Context.Message.ReferencedMessage as SocketMessage;
            if (message == null)
                await ReplyAsync("__**Ошибка**__ для работы команды требуется **ответить**" +
                    " на сообщение, которое вы хотите удалить");
            else if (seconds == null || !ulong.TryParse(seconds, out time))
                await ReplyAsync("__**Ошибка**__ время не задано или является отрицательным число.");
            else if (time < 0)
                await ReplyAsync("__**Ошибка**__ временной промежуток введён некорректно.");
            else
            {
                await Context.Message.DeleteAsync();
                await Task.Delay(TimeSpan.FromSeconds(time));
                await message.DeleteAsync();
            }
        }
        [Command ("list")]
        public async Task List(params string[] text)
        {
            if(text.Length == 0)
                await ReplyAsync("__**Ошибка**__ отсутствует текст.");
            else
            {
                string list = "";
                uint number = 1;
                foreach(string i in text)
                {
                    list += $"{number.ToString()}. {i}\n";
                    number++;
                }
                await ReplyAsync(list);
            }
        }
        [Command ("textReact")]
        public async Task TextReact(params string[] text)
        {
            if(text.Length == 0)
                await ReplyAsync("__**Ошибка**__ отсутствует строка.");
            else
            {
                string newtext = text[0].ToUpper();
                SocketMessage message = Context.Message.ReferencedMessage as SocketMessage;
                if(message == null)
                {
                    await ReplyAsync("__**Ошибка**__ при вызове команды следует ответи на сообщение(кнопка Reply)");
                    return;
                }
                Dictionary<string, Emoji> emotes = new Dictionary<string, Emoji>
                {
                    {"0", new Emoji("0️⃣")},
                    {"1", new Emoji("1️⃣")},
                    {"2", new Emoji("2️⃣")},
                    {"3", new Emoji("3️⃣")},
                    {"4", new Emoji("4️⃣")},
                    {"5", new Emoji("5️⃣")},
                    {"6", new Emoji("6️⃣")},
                    {"7", new Emoji("7️⃣")},
                    {"8", new Emoji("8️⃣")},
                    {"9", new Emoji("9️⃣")},
                    {"A", new Emoji("🇦")},
                    {"B", new Emoji("🇧")},
                    {"C", new Emoji("🇨")},
                    {"D", new Emoji("🇩")},
                    {"E", new Emoji("🇪")},
                    {"F", new Emoji("🇫")},
                    {"G", new Emoji("🇬")},
                    {"H", new Emoji("🇭")},
                    {"I", new Emoji("🇮")},
                    {"J", new Emoji("🇯")},
                    {"K", new Emoji("🇰")},
                    {"L", new Emoji("🇱")},
                    {"M", new Emoji("🇲")},
                    {"N", new Emoji("🇳")},
                    {"O", new Emoji("🇴")},
                    {"P", new Emoji("🇵")},
                    {"Q", new Emoji("🇶")},
                    {"R", new Emoji("🇷")},
                    {"S", new Emoji("🇸")},
                    {"T", new Emoji("🇹")},
                    {"U", new Emoji("🇺")},
                    {"V", new Emoji("🇻")},
                    {"W", new Emoji("🇼")},
                    {"X", new Emoji("🇽")},
                    {"Y", new Emoji("🇾")},
                    {"Z", new Emoji("🇿")},
                    {"#", new Emoji("#️⃣")},
                    {"*", new Emoji("*️⃣")},
                    {"10", new Emoji("🔟")},
                    {"NG", new Emoji("🆖")},
                    {"OK", new Emoji("🆗")},
                    {"UP", new Emoji("🆙")},
                    {"AB", new Emoji("🆎")},
                    {"CL", new Emoji("🆑")},
                    {"SOS", new Emoji("🆘")}
                };//10 NG OK UP p a AB b CL o SOS
                int length = newtext.Length;
                string buffer = "";
                List<string> emojis= new List<string>();
                for(int i = 0; i < length; i++)
                {
                    if (newtext[i] == '1'){
                        buffer += "1";
                        if (i + 1 < length && newtext[i + 1] == '0')
                        {
                            buffer += "0";
                            i++;
                        }
                    }
                    else if(newtext[i] == 'N')
                    {
                        buffer += "N";
                        if (i + 1 < length && newtext[i + 1] == 'G')
                        {
                            buffer += "NG";
                            i++;
                        }
                    }
                    else if(newtext[i] == 'O')
                    {
                        buffer += "O";
                        if (i + 1 < length && newtext[i + 1] == 'K')
                        {
                            buffer += "K";
                            i++;
                        }
                    }
                    else if (newtext[i] == 'U')
                    {
                        buffer += "U";
                        if (i + 1 < length && newtext[i + 1] == 'P')
                        {
                            buffer += "P";
                            i++;
                        }
                    }
                    else if (newtext[i] == 'A')
                    {
                        buffer += "A";
                        if (i + 1 < length && newtext[i + 1] == 'B')
                        {
                            buffer += "B";
                            i++;
                        }
                    }
                    else if (newtext[i] == 'C')
                    {
                        buffer += "C";
                        if (i + 1 < length && newtext[i + 1] == 'L')
                        {
                            buffer += "L";
                            i++;
                        }
                    }
                    else if (newtext[i] == 'S')
                    {
                        buffer += "S";
                        if (i + 2 < length && newtext[i + 1] == 'O' && newtext[i + 2] == 'S')
                        {
                            buffer += "OS";
                            i += 2;
                        }
                    }
                    else
                    {
                        buffer += newtext[i];
                    }
                    if (emojis.Contains(buffer))
                    {
                        await ReplyAsync("__**Ошибка**__ в тексте содержатся повторяющиеся элементы.");
                        return;
                    }
                    else
                    {
                        emojis.Add(buffer);
                        buffer = "";
                    }
                }
                foreach (string i in emojis)
                    await message.AddReactionAsync(emotes[i]);
            }
        }
        [Command ("help")]
        public async Task Help()
        {
            SocketUser bot;
            SocketGuild guild = Context.Guild;
            bot = guild.GetUser(918522063474552923);
            EmbedBuilder builder = new EmbedBuilder()
                .WithAuthor(bot.Username, bot.GetAvatarUrl())
                .AddField("Команды:", "1. __**wlayout**__ - смена раскладки сообщений(wlayout [input_text])\n2." +
                " __**wsendDelay**__ - отправка сообщений с задержкой(wsendDelay [seconds] [input_text[]])\n3." +
                " __**wdeleteDelay**__ - удаление отмеченного сообщения с задрежкой(wdeleteDelay [seconds])\n4." +
                " __**wlist**__ - создание списка из слов(wlist [input_text])\n5." +
                " __**wtextReact**__ - создание реакции на отмеченное сообщение с помощью эмодзи(wtextReact [input_text])\n6." +
                " __**whelp**__ - помощь(whelp)");
            await ReplyAsync(embed : builder.Build());
        }
    }
}
