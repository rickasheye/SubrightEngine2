namespace SubrightEditor.Nodes.Variables
{
    public class Integer : BaseObject
    {
        public int value;

        public Integer() : base("Integer")
        {
        }

        public void SetValue(int setvalue)
        {
            value = setvalue;
        }
    }
}