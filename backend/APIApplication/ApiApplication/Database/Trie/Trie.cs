namespace ApiApplication.Database.Trie;

public class Trie
{
    private TrieNode head;

    public Trie ()
    {
        head = new TrieNode();
    }
    
    public void AddWord(string word)
    {
        TrieNode curr = head;

        curr = curr.GetChild (word [0], true);

        for (int i = 1; i < word.Length; i++)
        {
            curr = curr.GetChild (word [i], true);
        }

        curr.SetTerminating();
    }
}