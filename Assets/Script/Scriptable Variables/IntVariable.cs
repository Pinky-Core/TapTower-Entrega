using System;
using UnityEngine;

// Permite crear este ScriptableObject desde el menú de Unity
[CreateAssetMenu(fileName = "New Int Variable", menuName = "Lobby Tools/ Scriptable Variables/ Int Variable")]
public class IntVariable : BaseScriptableVariable
{
    // Valor interno de la variable
    [SerializeField] int value = 0;

    // Evento que se dispara cuando el valor cambia
    public Action<int> onValueChange;

    // Suma un valor al actual y notifica el cambio
    public void AddValue(int newValue)
    {
        value += newValue;
        onValueChange?.Invoke(value); // Dispara el evento con el nuevo valor
    }

    // Establece un valor directamente y notifica el cambio
    public void SetValue(int newValue)
    {
        value = newValue;
        onValueChange?.Invoke(value); // Dispara el evento con el nuevo valor
    }

    // Retorna el valor actual
    public int GetValue()
    {
        return value;
    }

    // Guarda el valor en algún sistema de persistencia (probablemente archivo o PlayerPrefs)
    public override void SaveData()
    {
        IntVariableStruct temp = new IntVariableStruct { value = value };
        SaveData<IntVariableStruct>(temp); // Este método viene de BaseScriptableVariable
    }

    // Carga el valor desde el sistema de guardado
    public override void LoadData()
    {
        value = LoadData<IntVariableStruct>().value;
    }

    // Borra el archivo de guardado y reinicia el valor
    public override void EraseSaveFile()
    {
        base.EraseSaveFile();
        value = 0;
    }
}

// Estructura usada para serializar el valor al guardar/cargar
struct IntVariableStruct
{
    public int value;
}
