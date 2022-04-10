namespace ApiApplication.Database.Trie;

internal class TrieNode
{
    private LinkedList<TrieNode> children;

    public bool Terminating { private set; get; }
    public char Data { private set; get; }

    public TrieNode(char data = ' ')
    {
        this.Data = Data;
        Terminating = false;
        children = new LinkedList<TrieNode>();
    }

    public TrieNode GetChild(char c, bool createIfNotExist = false)
    {
        foreach (var child in children)
        {
            if (child.Data == c)
            {
                return child;
            }
        }

        if (createIfNotExist)
        {
            return CreateChild (c);
        }

        return null;
    }

    public void SetTerminating()
    {
        Terminating = true;
    }
			
    public TrieNode CreateChild(char c)
    {
        var child = new TrieNode (c);
        children.AddLast (child);

        return child;
    }
}