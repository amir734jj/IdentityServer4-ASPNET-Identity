namespace Models.Enums
{
    /// <summary>
    ///     Sort questions by:
    ///         1) no sorting
    ///         2) highest votes
    ///         3) most recent
    ///         4) number of answers
    /// </summary>
    public enum SortQuestionsByEnum
    {
        /// <summary>
        /// By setting this option to zero then: default(SortQuestionsByEnum) => None
        /// Because enums start from zero
        /// </summary>
        None = 0,
        Votes,
        Recent,
        Answers
    }
}