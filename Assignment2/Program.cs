
internal class Program
{
    public static Character player;

    static void Main(string[] args)
    {
        //게임시작
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
        // 아이템 정보 세팅
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayMyInventory();
                // 작업해보기
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        if (player.BasicAtk!=player.Atk)
            Console.WriteLine($"공격력 :{player.BasicAtk} (+{player.Atk-player.BasicAtk})");
        Console.WriteLine($"공격력 :{player.Atk}");
        if (5 - player.Def != 0)
            Console.WriteLine($"방어력 :{player.BasicDef} (+{player.Def - player.BasicDef})");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }
    static void DisplayMyInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        //플레이어 -> 인벤토리 -> 아이템 목록
        //내 인벤토리 보여줄려면 플레이어의 인벤토리 함수 호출 
        player.ShowMyInventoryInfo();

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        int input = CheckValidInput(0, 1);
        switch(input)
        {
            case 0 :
                DisplayGameIntro();
                break;
            case 1:
                DisplayEquipInfo();
                break;
        }
    }

    static void DisplayEquipInfo()
    {
        Console.Clear();

        Console.WriteLine("장착 관리");
        Console.WriteLine("보유 중인 아이템을 장착 및 해제할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("0. 나가기");
        player.ShowMyInventory();
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
    
                break;
            case 1:
                player.IsItemChose(1);
                DisplayMyInventory();
                break;
            case 2:
                player.IsItemChose(2);
                DisplayMyInventory();
                break;
        }

    }


    static int CheckValidInput(int min, int max)        //입력 숫자 판별함수
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
            Console.Write(">>");
        }
    }
    public class Character
    {
        public Inventory inventory { get; private set; }
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        public int Gold { get; }
        public int BasicAtk { get; set; }
        public int BasicDef { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            inventory = new Inventory();
            BasicAtk = atk;
            BasicDef = def;
        }
        public void ShowMyInventoryInfo()
        {
            inventory.ShowMyInventoryInfo();
        }
        public void ShowMyInventory()
        {
            inventory.ShowMyItems();
        }

        public void IsItemChose(int i)
        {
            inventory.IsChose(i);
        }
        public void SetItem(Item item)
        {
            if(item.IsChose==true)  //해제 해야함
            {
                Atk -= item.Atk;
                Def -= item.Def;
            }
            else    //장착
            {
                Atk += item.Atk;
                Def += item.Def;
            }

        }
    }

    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
            items.Add(new Item(false, "무쇠 갑옷", 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            items.Add(new Item(false, "낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다"));
        }

        public void ShowMyInventoryInfo()
        {
            foreach (Item item in items)
            {
                if (item.Atk == 0)
                {
                    if (item.IsChose == true)
                        Console.WriteLine($"- [E]{item.Name}\t| 방어력 +{item.Def} | {item.Explain}");
                    else
                        Console.WriteLine($"- {item.Name}\t| 방어력 +{item.Def} | {item.Explain}");
                }
                else if (item.Def == 0)
                {
                    if (item.IsChose == true)
                        Console.WriteLine($"- [E]{item.Name}\t| 공격력 +{item.Atk} | {item.Explain}");
                    else
                        Console.WriteLine($"- {item.Name}\t| 공격력 +{item.Atk} | {item.Explain}");

                }

            }
        }
        public void ShowMyItems()
        {
            int i = 1;
            foreach (Item item in items)
            {
                Console.WriteLine($"{i}. {item.Name}");
                i++;
            }
        }
        public void IsChose(int i)
        {
            if (items[i - 1].IsChose == true)
            {
                player.SetItem(items[i - 1]);
                items[i - 1].IsChose = false;
            }
            else
            {
                player.SetItem(items[i - 1]);
                items[i - 1].IsChose = true;
            }

        }
    }

    public class Item
    {
        //아이템 이름
        public bool IsChose { get; set; }
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public string Explain { get; }

        public Item(bool isChose, string name, int atk, int def, string explain)
        {
            IsChose = isChose;
            Name = name;
            Atk = atk;
            Def = def;
            Explain = explain;
        }


        //공격력 or 방어력
        //장착되었는지 아닌지
    }
}


