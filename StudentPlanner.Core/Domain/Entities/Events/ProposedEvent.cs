using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValueObjects;

namespace Entities.Events;

public class ProposedEvent //deprecated
{
    public required EventDetails Details { get; set; }
}
