﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SubscriberDtos
{
    public class CreateSubscriberDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime SubscribeDate { get; set; }
    }
}
