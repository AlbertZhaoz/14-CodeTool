using Masuit.LuceneEFCore.SearchEngine;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSearchDemo.Database
{
    /// <summary>
    /// ����
    /// </summary>
    [Table("Post")]
    public class Post : LuceneIndexableBaseEntity
    {
        public Post()
        {
            PostDate = DateTime.Now;
        }

        /// <summary>
        /// ����
        /// </summary>
        [Required(ErrorMessage = "���±��ⲻ��Ϊ�գ�"), LuceneIndex]
        public string Title { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required, MaxLength(24, ErrorMessage = "�������֧��24���ַ���"), LuceneIndex]
        public string Author { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required(ErrorMessage = "�������ݲ���Ϊ�գ�"), LuceneIndex(IsHtml = true)]
        public string Content { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Required(ErrorMessage = "�������䲻��Ϊ�գ�"), LuceneIndex]
        public string Email { get; set; }

        /// <summary>
        /// ��ǩ
        /// </summary>
        [StringLength(256, ErrorMessage = "��ǩ�������255���ַ�"), LuceneIndex]
        public string Label { get; set; }

        /// <summary>
        /// ���¹ؼ���
        /// </summary>
        [StringLength(256, ErrorMessage = "���¹ؼ����������255���ַ�"), LuceneIndex]
        public string Keyword { get; set; }

    }
}