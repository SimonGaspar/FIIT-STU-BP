namespace Bachelor_app.StereoVision
{
    /// <summary>
    /// Available algorithms for stereo correspondence.
    /// </summary>
    public enum EStereoCorrespondenceAlgorithm
    {
        StereoBM,
        StereoSGBM,
        CudaStereoBM,
        CudaStereoConstantSpaceBP
    }
}
