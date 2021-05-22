namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category
{
    public class InsertCategoryRequest
    {
        /// <summary>
        ///     Name of the category
        /// <example>Category 1</example>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Code of the category
        /// <example>Category 1</example>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Parent category id of the category
        /// <example>Category 1</example>
        /// </summary>
        public string ParentCategoryId { get; set; }
    }
}