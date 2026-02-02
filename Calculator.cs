using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;

namespace CalculatorDomainDemo;

/// <summary>
/// Owns calculator behaviour and internal state.
/// 
/// This class:
/// - Performs calculations
/// - Applies business rules
/// - Maintains history
/// 
/// Booking analogy:
/// similar to a booking logic / rules component.
/// </summary>
public class Calculator
{
    /*
     * INTERNAL MUTABLE STATE
     * 
     * This list is intentionally mutable.
     * The calculator changes it over time.
     */
    private readonly List<CalculationRequest> _history = new();

    public string Name { get; }
    public int LastResult { get; private set; }

    public Calculator(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name.");

        Name = name;
    }

    /*
     * ============================
     * HISTORY ACCESS (COPY)
     * ============================
     * 
     * IMPORTANT DESIGN CHOICE:
     * 
     * We return a COPY of the list,
     * not the internal list itself.
     * 
     * This means:
     * - External code cannot observe live mutation
     * - External code cannot affect internal state
     * - The calculator fully controls its data
     * 
     * Trade-off:
     * - Slightly more memory usage
     * - Stronger safety and predictability
     */
    public IReadOnlyList<CalculationRequest> GetHistory()
    {
        return _history.ToList(); // defensive copy
    }

    /*
     * ============================
     * CORE BEHAVIOUR
     * ============================
     */
    public int Calculate(int a, int b, OperationType operation)
    {
        // Guard clause: fail fast
        if (operation == OperationType.Divide && b == 0)
            throw new InvalidOperationException("Cannot divide by zero.");

        int result = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,
            _ => throw new InvalidOperationException("Invalid operation.")
        };

        // MUTATION happens here (internally only)
        _history.Add(new CalculationRequest(a, b, operation));

        LastResult = result;
        return result;


    }

public CalculationRequest GetCalculationRequest()
    {
        if(_history.Any())
        {
            throw new InvalidOperationException("No history to pull from");
        }
        else
        {
           return _history.Last(); 
        }
        CalculationRequest request = _history.Last();  
        return request; 
    }
    /*
     * ============================
     * LINQ AS QUESTIONS
     * ============================
     */

    public bool HasUsedDivision()
    {
        return _history.Any(r => r.Operation == OperationType.Divide);
    }

    public CalculationRequest? GetLastCalculation()
    {
        return _history.LastOrDefault();
    }

    public IEnumerable<CalculationRequest> GetByOperation(OperationType operation)
    {
        return _history.Where(r => r.Operation == operation);
    }

    /*
     * ============================
     * GROUPING WITH DICTIONARY
     * ============================
     */
    public Dictionary<OperationType, List<CalculationRequest>> GroupByOperation()
    {
        var grouped = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach (var request in _history)
        {
            if (!grouped.ContainsKey(request.Operation))
            {
                grouped[request.Operation] = new List<CalculationRequest>();
            }

            grouped[request.Operation].Add(request);
        }

        return grouped;
    }

    //writting history to file 
    public async Task SaveHistoryAsync(string filepath)
    {
        List<CalculationRequest> snapshot = _history.ToList(); //makes a copy of the history we have 
        string json = JsonSerializer.Serialize(snapshot); //
        await File.WriteAllTextAsync(filepath, json); //
         /*
      try
        {
            await Calculator.SaveHistoryAsync("calculator_history.json");
            Console.WriteLine("History saved ");  
        }

        catch (Exception ex)
        {
            Console.WriteLine("Faile to save calculator history");
        }
        
    }
    public async  Task<List<CalculationHistoryException>

      if (File.Exists(filepath))
      {
             string json = JsonSerializer.Deserializeerialize(snapshot); //
      }

      else {
        throw new                                                      
      }
     */
        // Using saving history 
    }
}