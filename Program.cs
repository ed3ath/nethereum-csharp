using System.IO;
using System.Text.Json;
using Nethereum.Web3;
using System.Numerics;
using Nethereum.Util;

class Program
{
    static void Main(string[] args)
    {
        getAccountWarriors().Wait();
        Console.ReadLine();
    }

    static async Task getAccountWarriors()
    {
        
        string filePath = "warrior.json";

        var abi = File.ReadAllText(filePath);

        string contractAddress = "0xD582f833bEa231Cf42A656688857AD4d1AF1837f"; // smart contract address
        string walletAddress = "0x4b6FFd37397014a95e4C983f845b4fc989bd9eb6"; // user wallet address

        var web3 = new Web3("https://rpc.ankr.com/fantom_testnet"); // rpc url

        var contract = web3.Eth.GetContract(abi, contractAddress); // Replace "abi" with the ERC20 contract ABI

        var getWarriorFunction = contract.GetFunction("getUserWarriors");
        var warriors = await getWarriorFunction.CallAsync<List<BigInteger>>(walletAddress);

        warriors.ForEach(e => {
            Console.WriteLine("Warrior #:" + e.ToString()); 
            getWarriorMetadata(e).Wait();
        });        
    }

    static async Task getWarriorMetadata(BigInteger warrior) {
        string filePath = "warrior.json";

        var abi = File.ReadAllText(filePath);

        string contractAddress = "0xD582f833bEa231Cf42A656688857AD4d1AF1837f"; // smart contract address

        var web3 = new Web3("https://rpc.ankr.com/fantom_testnet"); // rpc url

        var contract = web3.Eth.GetContract(abi, contractAddress); // Replace "abi" with the ERC20 contract ABI

        var getMetadataFunction = contract.GetFunction("tokenURI");
        var metadata = await getMetadataFunction.CallAsync<string>(warrior);
        Console.WriteLine("Warrior Metadata URL:" + metadata); 
    }
}