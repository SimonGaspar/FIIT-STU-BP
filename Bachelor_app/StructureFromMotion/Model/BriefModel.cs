namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for BRIEF
    /// </summary>
    public class BriefModel
    {
        public int DescriptorSize { get; private set; }

        public BriefModel(int descriptorSize = 32)
        {
            DescriptorSize = descriptorSize;
        }
    }
}
