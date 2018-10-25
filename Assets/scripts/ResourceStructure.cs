public class ResourceStructure 
{
	private string tag;
	private float supplyNumber;
	private float supplyMax;


	public ResourceStructure()
	{
		tag = "";
		supplyNumber = 0;
		supplyMax = 0;
	}


	public ResourceStructure(string newTag, float currentSupply, float setMax)
	{
		tag = newTag;
		supplyNumber = currentSupply;
		supplyMax = setMax;
	}


	public static ResourceStructure operator+ (ResourceStructure x, ResourceStructure y)
	{
		if(x.supplyMax - x.supplyNumber <= x.supplyNumber + y.supplyNumber)
		{
			x.supplyNumber = x.supplyNumber + y.supplyNumber;
		}
		else if (x.supplyNumber < x.supplyMax)
		{
			x.supplyNumber = x.supplyNumber - x.supplyMax;
		}

		return x;
	}


	public static ResourceStructure operator+ (ResourceStructure x, float y)
	{
		if(x.supplyMax - x.supplyNumber <= x.supplyNumber + y)
		{
			x.supplyNumber = x.supplyNumber + y;
		}
		else if (x.supplyNumber < x.supplyMax)
		{
			x.supplyNumber = x.supplyNumber - x.supplyMax;
		}

		return x;
	}


	public static ResourceStructure operator- (ResourceStructure x, ResourceStructure y)
	{
		if(x.supplyMax - x.supplyNumber <= x.supplyNumber + y.supplyNumber)
		{
			x.supplyNumber = x.supplyNumber - y.supplyNumber;
		}
		else if (x.supplyNumber < x.supplyMax)
		{
			x.supplyNumber = x.supplyNumber - x.supplyMax;
		}

		return x;
	}


	public static ResourceStructure operator- (ResourceStructure x, float y)
	{
		if(0 < x.supplyNumber - y)
		{
			x.supplyNumber = x.supplyNumber - y;
		}

		return x;
	}
}
