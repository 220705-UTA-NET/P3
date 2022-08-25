using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server_Database;
using Server_DataModels;

namespace server.Controllers
{
    [Route("API/Transactions")]
    [ApiController]
    public class CONTROLLER_Transactions : ControllerBase
    {
        // FIELDS
        private readonly IRepository API_PROP_IRepository;
        private readonly ILogger<CONTROLLER_Transactions> API_PROP_Logger;

        // CONSTRUCTORS
        public CONTROLLER_Transactions(IRepository INPUT_IRepository, ILogger<CONTROLLER_Transactions> INPUT_Logger)
        {
            this.API_PROP_IRepository = INPUT_IRepository;
            this.API_PROP_Logger = INPUT_Logger;
        }

        // FIELDS
        [HttpPost]
        [Route("GetTransactionHistory")]
        public async Task<DTO_TransactionHistory> GetTransactionHistoryAsync(int INPUT_AuthToken, [FromBody] int INPUT_AccountNumber)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            DTO_TransactionHistory OUTPUT_DTO = new DTO_TransactionHistory();

            List<DMODEL_Transaction> TEMP_LIST_TransactionHistory = new List<DMODEL_Transaction>();
            TEMP_LIST_TransactionHistory = await API_PROP_IRepository.TRANSACTION_ASYNC_getTransactons(INPUT_AccountNumber);

            // Checks if returned list is a dummy list
            if (TEMP_LIST_TransactionHistory[0].transaction_id == -1)
            {
                OUTPUT_DTO.NumberOfTransactions = -1;
                OUTPUT_DTO.LIST_DMODEL_Transactions = new List<DMODEL_Transaction>();
            }
            else
            {
                OUTPUT_DTO.NumberOfTransactions = TEMP_LIST_TransactionHistory.Count;
                OUTPUT_DTO.LIST_DMODEL_Transactions = TEMP_LIST_TransactionHistory;
            }

            return OUTPUT_DTO;
        }
    }
}
