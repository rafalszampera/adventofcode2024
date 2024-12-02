<Query Kind="Program" />

void Main()
{	
	var safe_level_number = 0;
		
	FileStream fileStream = new FileStream(@"input", FileMode.Open);
	using (StreamReader reader = new StreamReader(fileStream))
	{
        string line = String.Empty;
		while ((line = reader.ReadLine()) != null)
	    {
			var numbers = line.Split(' ');
			var prev_number = Convert.ToInt32(numbers[0]);
			var dec = false;
			var inc = false; 
			var unsafe_level = false;
			
			for(var i = 1; i < numbers.Length; i++)
			{
				var current_number = Convert.ToInt32(numbers[i]);
				if(prev_number - current_number > 0 && !inc) 
				{
					dec = true;
				}
				else if(prev_number - current_number < 0 && !dec) 
				{
					inc = true;
				}
				else
				{
					unsafe_level = true;
				}	
				
				if(Math.Abs(prev_number - current_number) <= 3)
				{
					prev_number = current_number;
				}
				else
				{
					unsafe_level = true;
				}	
							
			}
			safe_level_number += unsafe_level ? 0 : 1;
		}
	}
	safe_level_number.Dump();
}
