using DuploCloud_WeatherForecast_Data.Enums;
using System.Collections;

namespace DuploCloud_WeatherForecast.OptionsManagers;

public class CurrentOptionsManager : IEnumerable, ICollection<CurrentOptionsParameter>
{
    public static CurrentOptionsManager All { get { return new CurrentOptionsManager((CurrentOptionsParameter[])Enum.GetValues(typeof(CurrentOptionsParameter))); } }
    public List<CurrentOptionsParameter> Parameter { get { return new List<CurrentOptionsParameter>(_parameter); } }

    public int Count => _parameter.Count;

    public bool IsReadOnly => false;

    private readonly List<CurrentOptionsParameter> _parameter = new List<CurrentOptionsParameter>();

    public CurrentOptionsManager()
    {

    }

    public CurrentOptionsManager(CurrentOptionsParameter parameter)
    {
        Add(parameter);
    }

    public CurrentOptionsManager(CurrentOptionsParameter[] parameters)
    {
        Add(parameters);
    }
    public CurrentOptionsParameter this[int index]
    {
        get { return _parameter[index]; }
        set
        {
            _parameter[index] = value;
        }
    }

    public void Add(CurrentOptionsParameter param)
    {
        // Check that the parameter if it is added
        if (_parameter.Contains(param)) return;

        _parameter.Add(param);
    }

    public void Add(CurrentOptionsParameter[] parameters)
    {
        foreach (CurrentOptionsParameter parameter in parameters)
        {
            Add(parameter);
        }
    }

    public void Clear()
    {
        _parameter.Clear();
    }

    public bool Contains(CurrentOptionsParameter item)
    {
        return _parameter.Contains(item);
    }

    public bool Remove(CurrentOptionsParameter item)
    {
        return _parameter.Remove(item);
    }

    public void CopyTo(CurrentOptionsParameter[] array, int arrayIndex)
    {
        _parameter.CopyTo(array, arrayIndex);
    }

    public IEnumerator<CurrentOptionsParameter> GetEnumerator()
    {
        return _parameter.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
