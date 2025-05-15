using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rawy.Dtos.Base;

namespace Rawy.Dtos.User
{
    public class UserResponseDto : BaseDto
    {
        public string Id { get; set; }
        
        public string DisplayName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string ProfilePictureUrl { get; set; }
        
        public string CvUrl { get; set; }
        
        public string Token { get; set; }
        
        public int FavoritesCount { get; set; }
        
        public int ReviewsCount { get; set; }
        
        public int RecordingsCount { get; set; }
    }
} 