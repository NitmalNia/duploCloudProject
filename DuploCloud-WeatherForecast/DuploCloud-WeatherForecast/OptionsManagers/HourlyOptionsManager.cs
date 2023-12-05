using DuploCloud_WeatherForecast_Data.Enums;
using System.Collections;

namespace DuploCloud_WeatherForecast.OptionsManagers;

public class HourlyOptionsManager : IEnumerable<HourlyOptionsParameter>, ICollection<HourlyOptionsParameter>
{
    public static HourlyOptionsManager All { get { return new HourlyOptionsManager((HourlyOptionsParameter[])Enum.GetValues(typeof(HourlyOptionsParameter))); } }
    public List<HourlyOptionsParameter> Parameter { get { return new List<HourlyOptionsParameter>(_parameter); } }

    public int Count => _parameter.Count;

    public bool IsReadOnly => false;

    private readonly List<HourlyOptionsParameter> _parameter;

    public HourlyOptionsManager(HourlyOptionsParameter parameter)
    {
        _parameter = new List<HourlyOptionsParameter>();
        Add(parameter);
    }

    public HourlyOptionsManager(HourlyOptionsParameter[] parameter)
    {
        _parameter = new List<HourlyOptionsParameter>();
        Add(parameter);
    }

    public HourlyOptionsManager()
    {
        _parameter = new List<HourlyOptionsParameter>();
    }

    public HourlyOptionsParameter this[int index]
    {
        get { return _parameter[index]; }
        set
        {
            _parameter[index] = value;
        }
    }

    public void Add(HourlyOptionsParameter param)
    {
        // Check that the parameter if it is added
        if (this._parameter.Contains(param)) return;

        _parameter.Add(param);
    }

    public void Add(HourlyOptionsParameter[] param)
    {
        foreach (HourlyOptionsParameter paramToAdd in param)
        {
            Add(paramToAdd);
        }
    }

    public IEnumerator<HourlyOptionsParameter> GetEnumerator()
    {
        return _parameter.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public void Clear()
    {
        _parameter.Clear();
    }

    public bool Contains(HourlyOptionsParameter item)
    {
        return _parameter.Contains(item);
    }

    public void CopyTo(HourlyOptionsParameter[] array, int arrayIndex)
    {
        _parameter.CopyTo(array, arrayIndex);
    }

    public bool Remove(HourlyOptionsParameter item)
    {
        return _parameter.Remove(item);
    }
}
