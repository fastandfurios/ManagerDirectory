using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerDirectory.Repository;
using ManagerDirectory.Validation;

namespace ManagerDirectory.IO
{
    public class InputData
    {
	    private string _entry;

		/// <summary>
		/// Дает пользователю ввести строку
		/// </summary>
		/// <param name="defaultPath">Путь по умолчанию</param>
		/// <param name="checker">Проверщик</param>
		/// <returns>Строка, введенная пользователем</returns>
	    public string Input(string defaultPath, Checker checker)
	    {
		    do
		    {
				Console.Write($"{defaultPath} > ");
				_entry = Console.ReadLine();
		    } while (!checker.CheckInputCommand(_entry));

		    return _entry;
	    }

		public string Input(Checker checker)
		{
			Console.Write($"Continue y/n: ");
			_entry = Console.ReadLine();

			if (checker.CheckInputCommand(_entry))
				return _entry;
			
			return _entry;
		}
    }
}
