using DuploCloud_WeatherForecast_Data.Enums;
using System.Collections;
using System.Collections.Generic;

namespace DuploCloud_WeatherForecast.OptionsManagers;

public class DailyOptionsManager : IEnumerable, ICollection<DailyOptionsParameter>
{
    public static DailyOptionsManager All { get { return new DailyOptionsManager((DailyOptionsParameter[])Enum.GetValues(typeof(DailyOptionsParameter))); } }

    public List<DailyOptionsParameter> Parameter { get { return new List<DailyOptionsParameter>(_parameter); } }

    public int Count => _parameter.Count;

    public bool IsReadOnly => false;

    private readonly List<DailyOptionsParameter> _parameter = new List<DailyOptionsParameter>();

    public DailyOptionsManager()
    {

    }

    public DailyOptionsManager(DailyOptionsParameter parameter)
    {
        Add(parameter);
    }

    public DailyOptionsManager(DailyOptionsParameter[] parameter)
    {
        Add(parameter);
    }

    public DailyOptionsParameter this[int index]
    {
        get { return _parameter[index]; }
        set
        {
            _parameter[index] = value;
        }
    }

    public void Add(DailyOptionsParameter param)
    {
        // Check that the parameter if it is added
        if (_parameter.Contains(param)) return;

        _parameter.Add(param);
    }

    public void Add(DailyOptionsParameter[] param)
    {
        foreach (DailyOptionsParameter paramToAdd in param)
        {
            Add(paramToAdd);
        }
    }

    public void Clear()
    {
        _parameter.Clear();
    }

    public bool Contains(DailyOptionsParameter item)
    {
        return _parameter.Contains(item);
    }

    public bool Remove(DailyOptionsParameter item)
    {
        return _parameter.Remove(item);
    }

    public void CopyTo(DailyOptionsParameter[] array, int arrayIndex)
    {
        _parameter.CopyTo(array, arrayIndex);
    }

    public IEnumerator<DailyOptionsParameter> GetEnumerator()
    {
        return _parameter.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
