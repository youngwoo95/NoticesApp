using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoticeApp.Models
{
    [Table("Notices")]
    public class Notice
    {
        /// <summary>
        /// Serial Number
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 외래키
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        [Required(ErrorMessage = "이름을 입력해주세요.")]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 상단 고정: 공지글로 올리기
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// 등록자: CreateBy, Creator
        /// </summary>
        public  string CreateBy{ get; set; }

        /// <summary>
        /// 등록일: Created
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// 수정자: LastModifiedBy, ModifiedBy
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 수정일: LastModified, Modified
        /// </summary>
        public DateTime? Modified { get; set; }
    }
}
