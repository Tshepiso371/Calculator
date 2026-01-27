namespace CalculatorDomainDemo;

/// <summary>
/// This class represents the DOMAIN BEHAVIOUR.
/// 
/// In real systems:
/// - This is where rules live
/// - This is where decisions are made
/// 
/// In the booking system, this is similar to:
/// - Booking management logic
/// </summary>
public class Calculator
{
    /// <summary>
    /// This property stores state INSIDE the object.
    /// 
    /// Notice:
    /// - Public getter
    /// - Private setter
    /// 
    /// This means:
    /// - Other code can read the value
    /// - Only the Calculator can change it
    /// 
    /// This protects the object from invalid changes.
    /// </summary>
    /// 
    
    private readonly List<CalculationRequest> _History = new (); //27
    // Properties 
    public IReadOnlyList<CalculationRequest> History //27
    {
            get { return _History; }
    } 
    public int LastResult { get; private set; }

    /// <summary>
    /// Every calculator must have a name.
    /// 
    /// Constructors define what MUST exist
    /// for an object to be valid.
    /// </summary>
    public string Name { get; }

    public Calculator(string name)
    {
        // Guard clause:
        // We do NOT allow invalid objects to exist.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name");

        Name = name;
    }

    /// <summary>
    /// This method applies business rules.
    /// 
    /// It does NOT:
    /// - Read from the console
    /// - Print output
    /// 
    /// This separation is important because:
    /// - Console apps are temporary
    /// - Business logic must survive
    /// 
    /// In the booking system:
    /// - This would decide if a booking is allowed
    /// - This would enforce capacity rules
    /// </summary>
    public int Calculate(int a, int b, OperationType operation)
    {
        // Switch expression ensures ALL enum cases are handled
        LastResult = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,


            // This should never happen if enums are used correctly
            _ => throw new InvalidOperationException("Invalid operation")
        };

        CalculationRequest request = new CalculationRequest(a, b, operation); // 27
        _History.Add(request); // 27  (Add to our history)

        return LastResult;
    }

    // How many Additions were done on this calculator(//27)
    public List<CalculationRequest>ReturnAdditions()
    {
        List<CalculationRequest> additions = new List<CalculationRequest>();
        foreach (CalculationRequest req in _History)
        {
            if (req.Operation == OperationType.Add)
            {
                additions.Add(req);
            }
        }
        return additions; 
    }
        public List<CalculationRequest> ReturnAdditions2()
    {
       //  List<CalculationRequest> req = new List<CalculationRequest>(); (This method and the following one do the same thing but in different ways)
        List<CalculationRequest> req = _History.
        Where(i => i.Operation == OperationType.Add). // LINQ
        ToList();
           return req;

    }

    // Has division been used? (27)
    /*
    public List<CalculationRequest> ReturnDivisions()
    {
        List<CalculationRequest> divisions = new List<CalculationRequest>();  ( Didint answer the question as asked)
        foreach (CalculationRequest req in _History)
        {
            if (req.Operation == OperationType.Divide)
            {
                divisions.Add(req);
            }
        }
        return divisions;
    }
    */

    public bool HasDivisionBeenUsed()
    {
        bool hasDivision =_History.Any(r => r.Operation == OperationType.Divide); // LINQ
        return hasDivision;
    }

     // Writing custom Exception (27)

     public double Divide(CalculationRequest request)
     {
        if(request.B == 0)
        {
            throw new InvalidOperationException("division cannot be done because the denominator is zero");
        }
        else
        {
            return request.A /request.B;
        }
     }

     // Group our calculation requests
     public Dictionary<OperationType, List<CalculationRequest>> Grouped()
    {
        Dictionary<OperationType, List<CalculationRequest>> grouped = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach(CalculationRequest req in _History)
        {
            if (!grouped.ContainsKey(req.Operation))     // 
            {
                grouped[req.Operation] = new List<CalculationRequest>();
            }
            grouped[req.Operation].Add(req);
        }

        return grouped;
    }

}