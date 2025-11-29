/*
 * FILENAME:        SearchCriteria.cs
 * ASSIGNMENT:      Advanced Software Quality - Final Project
 * DESCRIPTION:     Data model for 
 */

namespace GroundTerminalSystem.Models
{
    public class SearchCriteria
    {
        public string TailNumber { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public SearchCriteria(string tailNumber, DateTime startTime, DateTime endTime)
        {
            TailNumber = tailNumber;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
