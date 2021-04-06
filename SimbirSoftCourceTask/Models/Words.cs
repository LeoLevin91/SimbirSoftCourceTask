namespace SimbirSoftGUI.Models
{
    public class Words
    {
        public string Word { set; get; }
        public int Count { set; get; }

        public Words(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}