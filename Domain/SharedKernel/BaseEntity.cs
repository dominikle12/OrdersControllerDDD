﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    public abstract class BaseEntity<TId>
    {   
        public TId Id { get; protected set; }
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
