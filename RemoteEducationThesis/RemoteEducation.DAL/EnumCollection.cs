namespace Education.DAL
{
    public abstract class EnumCollection
    {
        #region Enum

        #region Role

        /// <summary>
        /// Role types
        /// </summary>
        public enum RoleType
        {
            /// <summary>
            /// Admin role
            /// </summary>
            Admin = 1,

            /// <summary>
            /// Professor role
            /// </summary>
            Professor = 2,

            /// <summary>
            /// Student role
            /// </summary>
            Student = 3
        }

        #endregion

        #endregion
    }
}
