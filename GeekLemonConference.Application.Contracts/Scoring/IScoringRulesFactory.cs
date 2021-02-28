﻿using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Contracts
{
    public interface IScoringRulesFactory
    {
        public ScoringRules DefaultSet { get; }
    }
}
