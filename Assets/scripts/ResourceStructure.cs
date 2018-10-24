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
		x.supplyNumber += y.supplyNumber;

		return x;
	}


	public static ResourceStructure operator- (ResourceStructure x, ResourceStructure y)
	{
		x.supplyNumber -= y.supplyNumber;

		return x;
	}
}
