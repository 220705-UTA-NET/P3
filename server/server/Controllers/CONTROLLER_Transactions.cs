using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Data;
using server.Model;

namespace server.Controllers
{
    [Route("API/Transactions")]
    [ApiController]
    public class CONTROLLER_Transactions : ControllerBase
    {
        // FIELDS
        private readonly TRANSACTION_IRepository API_PROP_IRepository;
        private readonly ILogger<CONTROLLER_Transactions> API_PROP_Logger;
        private readonly int BASE_BankAccountID;

        // CONSTRUCTORS
        public CONTROLLER_Transactions(TRANSACTION_IRepository INPUT_IRepository, ILogger<CONTROLLER_Transactions> INPUT_Logger)
        {
            this.API_PROP_IRepository = INPUT_IRepository;
            this.API_PROP_Logger = INPUT_Logger;
            BASE_BankAccountID = 1;
        }

        // ===================================== METHODS ====================================================================================================
        [HttpGet]
        [Route("TransactionHistory")]
        public async Task<DTO_TRANSACTION_TransactionHistory> TRANSACTION_MAIN_ASYNC_GetTransactionHistory(int INPUT_AuthToken, int INPUT_AccountNumber)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            DTO_TRANSACTION_TransactionHistory OUTPUT_DTO = new DTO_TRANSACTION_TransactionHistory();

            List<DMODEL_Transaction> TEMP_LIST_TransactionHistory = new List<DMODEL_Transaction>();
            TEMP_LIST_TransactionHistory = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetTransactonHistory(INPUT_AccountNumber);

            // Checks if returned list is a dummy list
            if (TEMP_LIST_TransactionHistory[0].transaction_id == -1)
            {
                OUTPUT_DTO.NumberOfTransactions = 0;
                OUTPUT_DTO.LIST_DMODEL_Transactions = new List<DMODEL_Transaction>();
            }
            else
            {
                OUTPUT_DTO.NumberOfTransactions = TEMP_LIST_TransactionHistory.Count;
                OUTPUT_DTO.LIST_DMODEL_Transactions = TEMP_LIST_TransactionHistory;
            }

            return OUTPUT_DTO;
        }

        // =================================================================================================================================================
        [HttpPost]
        [Route("Deposit")]
        public async Task<DTO_TRANSACTION_TransactionResponse> TRANSACTION_MAIN_ASYNC_DepositMoney(int INPUT_AuthToken, [FromBody] DTO_TRANSACTION_DepositWithdraw INPUT_DTO_Deposit)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            // Transfer Money from Implicit Bank Reserve to Account

            int STATUS_Deposit;
            STATUS_Deposit = await TRANSACTION_LOGIC_ASYNC_MoneyTransfer(BASE_BankAccountID, INPUT_DTO_Deposit.AccountID, INPUT_DTO_Deposit.ChangeAmount);

            //Successfuy Deposit
            if (STATUS_Deposit == 1)
            {
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_Deposit.AccountID, INPUT_DTO_Deposit.ChangeAmount, "DEPOSIT", true);
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(BASE_BankAccountID, INPUT_DTO_Deposit.ChangeAmount, "DEPOSIT for Account: " + INPUT_DTO_Deposit.AccountID, false);
            }

            DTO_TRANSACTION_TransactionResponse OUTPUT_DTO = new DTO_TRANSACTION_TransactionResponse();
            OUTPUT_DTO.StatusCode = STATUS_Deposit;
            OUTPUT_DTO.AccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_DTO_Deposit.AccountID);
            return OUTPUT_DTO;
        }

        // =================================================================================================================================================
        [HttpPost]
        [Route("Withdraw")]
        public async Task<DTO_TRANSACTION_TransactionResponse> TRANSACTION_MAIN_ASYNC_WithdrawMoney(int INPUT_AuthToken, [FromBody] DTO_TRANSACTION_DepositWithdraw INPUT_DTO_Withdraw)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            // Transfer Money from Account to Implicit Bank Reserve 

            int STATUS_Withdraw;
            STATUS_Withdraw = await TRANSACTION_LOGIC_ASYNC_MoneyTransfer(INPUT_DTO_Withdraw.AccountID, BASE_BankAccountID, INPUT_DTO_Withdraw.ChangeAmount);

            //Successfuy Deposit
            if (STATUS_Withdraw == 1)
            {
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_Withdraw.AccountID, INPUT_DTO_Withdraw.ChangeAmount, "DEPOSIT", true);
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(BASE_BankAccountID, INPUT_DTO_Withdraw.ChangeAmount, "DEPOSIT for Account: " + INPUT_DTO_Withdraw.AccountID, false);
            }

            DTO_TRANSACTION_TransactionResponse OUTPUT_DTO = new DTO_TRANSACTION_TransactionResponse();
            OUTPUT_DTO.StatusCode = STATUS_Withdraw;
            OUTPUT_DTO.AccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_DTO_Withdraw.AccountID);
            return OUTPUT_DTO;
        }

        // =================================================================================================================================================
        [HttpPost]
        [Route("RequestCreate")]
        public async Task<int> TRANSACTION_MAIN_ASYNC_MakeNewRequest(int INPUT_AuthToken, [FromBody] DTO_TRANSACTION_RequestCreate INPUT_DTO_RequestCreate)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            // Parse and Validate CustomerEmail->CustomerID
            if (INPUT_DTO_RequestCreate.reciever_email == "")
            {
                return -2;
            }
            int WORK_CustomerID = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetCustomerIDFromEmail(INPUT_DTO_RequestCreate.reciever_email);

            if (WORK_CustomerID == -1)
            {
                return -2;
            }

            // Create Request Notes
            // NOTE: request_type (true = send money, false = request money)
            string WORK_RequestNotes;
            if( INPUT_DTO_RequestCreate.request_type == true)
            {
                WORK_RequestNotes = "Sending Money From: " + INPUT_DTO_RequestCreate.reciever_email + " Notes: " + INPUT_DTO_RequestCreate.request_notes;
            }
            else
            {
                WORK_RequestNotes = "Requesting Money From: " + INPUT_DTO_RequestCreate.reciever_email + " Notes: " + INPUT_DTO_RequestCreate.request_notes;
            }

            // Insert Request
            bool STATUS_RequestCreate;


            STATUS_RequestCreate = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewRequest(WORK_CustomerID, INPUT_DTO_RequestCreate.org_acct, INPUT_DTO_RequestCreate.amount, INPUT_DTO_RequestCreate.request_type, WORK_RequestNotes);

            // Basic Returns Until Error Codes Can Be Designated
            if (STATUS_RequestCreate == true)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        // =================================================================================================================================================
        [HttpGet]
        [Route("RequestOutstanding")]
        public async Task<DTO_TRANSACTION_RequestOutstanding> TRANSACTION_MAIN_ASYNC_GetOutstandingRequest(int INPUT_AuthToken, int INPUT_CustomerID)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            DTO_TRANSACTION_RequestOutstanding OUTPUT_DTO = new DTO_TRANSACTION_RequestOutstanding();

            List<DMODEL_Request> TEMP_LIST_OutstandingRequest = new List<DMODEL_Request>();
            TEMP_LIST_OutstandingRequest = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetOutstandingRequest(INPUT_CustomerID);

            // Checks if returned list is a dummy list
            if (TEMP_LIST_OutstandingRequest[0].request_id == -1)
            {
                OUTPUT_DTO.NumberOfRequests = 0;
                OUTPUT_DTO.LIST_DMODEL_Request = new List<DMODEL_Request>();
            }
            else
            {
                OUTPUT_DTO.NumberOfRequests = TEMP_LIST_OutstandingRequest.Count;
                OUTPUT_DTO.LIST_DMODEL_Request = TEMP_LIST_OutstandingRequest;
            }

            return OUTPUT_DTO;

        }

        // =================================================================================================================================================
        [HttpPost]
        [Route("RequestResponse")]
        public async Task<int> TRANSACTION_MAIN_ASYNC_UseRequestResponse(int INPUT_AuthToken, [FromBody] DTO_TRANSACTION_RequestResponse INPUT_DTO_RequestResponse)
        {
            // Authenticate User
            //NEEDS IMPLEMENTING WHEN LOGIN FEATURES ARE DONE

            // If User Choices to Decline Transfer
            if (INPUT_DTO_RequestResponse.ApprovedTransaction == false)
            {
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest(INPUT_DTO_RequestResponse.RequestData.request_id);
                return 2;
            }

            // IF User Approved Transfer
            // NOTE: request_type (true = send money, false = request money)
            int STATUS_Request;
            if (INPUT_DTO_RequestResponse.RequestData.request_type == true)
            {
                STATUS_Request = await TRANSACTION_LOGIC_ASYNC_MoneyTransfer(INPUT_DTO_RequestResponse.RequestData.org_acct, INPUT_DTO_RequestResponse.SelectedAccountID, INPUT_DTO_RequestResponse.RequestData.amount);

                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_RequestResponse.RequestData.org_acct, INPUT_DTO_RequestResponse.RequestData.amount, INPUT_DTO_RequestResponse.RequestData.request_notes, false);
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_RequestResponse.RequestData.request_from, INPUT_DTO_RequestResponse.RequestData.amount, INPUT_DTO_RequestResponse.RequestData.request_notes, true);
            }
            else
            {
                STATUS_Request = await TRANSACTION_LOGIC_ASYNC_MoneyTransfer(INPUT_DTO_RequestResponse.SelectedAccountID, INPUT_DTO_RequestResponse.RequestData.org_acct, INPUT_DTO_RequestResponse.RequestData.amount);

                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_RequestResponse.RequestData.org_acct, INPUT_DTO_RequestResponse.RequestData.amount, INPUT_DTO_RequestResponse.RequestData.request_notes, true);
                await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_InsertNewTransaction(INPUT_DTO_RequestResponse.RequestData.request_from, INPUT_DTO_RequestResponse.RequestData.amount, INPUT_DTO_RequestResponse.RequestData.request_notes, false);
            }

            await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_DeleteOutstandingRequest(INPUT_DTO_RequestResponse.RequestData.request_id);
            return STATUS_Request;
        }

        // ===================================== PRIVATE METHODS ===========================================================================================
        private async Task<int> TRANSACTION_LOGIC_ASYNC_MoneyTransfer(int INPUT_SenderAccount, int INPUT_RecieverAccount, double INPUT_ChangeAmount)
        {
            // Initial Verification
            double VERIFICATION_INITIAL_SenderAccountBalance;
            double VERIFICATION_INITIAL_RecieverAccountBalance;
            double VERIFICATION_POST_SenderAccountBalance;
            double VERIFICATION_POST_RecieverAccountBalance;

            try
            {
                    // Setting up Initial Verification Variables to proper format
                VERIFICATION_INITIAL_SenderAccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_SenderAccount);
                VERIFICATION_INITIAL_RecieverAccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_RecieverAccount);

                VERIFICATION_INITIAL_SenderAccountBalance = Math.Round(VERIFICATION_INITIAL_SenderAccountBalance, 2);
                VERIFICATION_INITIAL_RecieverAccountBalance = Math.Round(VERIFICATION_INITIAL_RecieverAccountBalance, 2);

                    // Initial Verification - AccountID Validity (Error Codes: -2 / -3 for Account Not Exists for Sender / Reciever Account)
                if (VERIFICATION_INITIAL_SenderAccountBalance == -1)
                {
                    API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer: (INPUT_SenderAccount {0}) --> OUTPUT: !FAILURE = Sender account {1} does not exist", INPUT_SenderAccount, INPUT_SenderAccount);
                    return -2;
                }
                else if (VERIFICATION_INITIAL_RecieverAccountBalance == -1)
                {
                    API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer: (INPUT_RecieverAccount {0}) --> OUTPUT: !FAILURE = Reciever account {2} does not exist", INPUT_RecieverAccount, INPUT_RecieverAccount);
                    return -3;
                }

                    // Initial Verification (Error Codes: -4 for Sender insufficent funds)
                if (VERIFICATION_INITIAL_SenderAccountBalance < INPUT_ChangeAmount)
                {
                    API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer: (INPUT_SenderAccount {0}, INPUT_ChangeAmount {1} --> OUTPUT: !FAILURE = Sender account has insufficent funds)", INPUT_SenderAccount, INPUT_ChangeAmount);
                    return -4;

                }
            }
            catch (Exception ERROR_IntialAccountVerification)
            {
                API_PROP_Logger.LogError("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer ({0} , {1} , {2}) --> OUTPUT: !ERROR = Issue with initial account verification", INPUT_SenderAccount, INPUT_RecieverAccount, INPUT_ChangeAmount);
                API_PROP_Logger.LogError(ERROR_IntialAccountVerification.Message, ERROR_IntialAccountVerification);
                return -1;
            }

            // Money Transfer and Post Verification
            try
            {
                // Money Transfer
                bool STATUS_SuccessfulTransferSender = false;
                bool STATUS_SuccessfulTransferReciever = false;

                double WORK_NewSenderBalance = Math.Round((VERIFICATION_INITIAL_SenderAccountBalance - INPUT_ChangeAmount), 2);
                double WORK_NewRecieverBalance = Math.Round((VERIFICATION_INITIAL_RecieverAccountBalance + INPUT_ChangeAmount), 2);

                STATUS_SuccessfulTransferSender = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_UpdateAccountBalence(INPUT_SenderAccount, WORK_NewSenderBalance);
                STATUS_SuccessfulTransferReciever = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_UpdateAccountBalence(INPUT_RecieverAccount, WORK_NewRecieverBalance);
                
                // DOUBLE Post Verification for Redundancy
                    // Basic Function OUTPUT Verification
                if (STATUS_SuccessfulTransferSender == false || STATUS_SuccessfulTransferReciever == false)
                {
                    throw new InvalidOperationException();
                }

                    // Manual RE-Calculate Verification
                VERIFICATION_POST_SenderAccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_SenderAccount);
                VERIFICATION_POST_RecieverAccountBalance = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_GetAccountBalance(INPUT_RecieverAccount);

                VERIFICATION_POST_SenderAccountBalance = Math.Round(VERIFICATION_POST_SenderAccountBalance, 2);
                VERIFICATION_POST_RecieverAccountBalance = Math.Round(VERIFICATION_POST_RecieverAccountBalance, 2);

                if (WORK_NewSenderBalance != VERIFICATION_POST_SenderAccountBalance || WORK_NewRecieverBalance != VERIFICATION_POST_RecieverAccountBalance)
                {
                    throw new InvalidOperationException();
                }

                // Run If Successful Completed Transaction and Verification
                return 1;

            }
            catch (Exception ERROR_PostAccountVerification)
            {
                API_PROP_Logger.LogError("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer ({0} , {1} , {2}) --> OUTPUT: !ERROR = Issue with post account verification, !!Reverting to Inital Verification State!!", INPUT_SenderAccount, INPUT_RecieverAccount, INPUT_ChangeAmount);
                API_PROP_Logger.LogError(ERROR_PostAccountVerification.Message, ERROR_PostAccountVerification);

                    // Tries to revert the transaction 3 time in case of tragic error
                int COUNTER_RevertTransaction = 0;
                bool STATUS_REVERTED_SenderAccount = false;
                bool STATUS_REVERTED_RecieverAccount = false;
                while (COUNTER_RevertTransaction < 3)
                {
                    if (STATUS_REVERTED_SenderAccount == false)
                    {
                        STATUS_REVERTED_SenderAccount = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_UpdateAccountBalence(INPUT_SenderAccount, VERIFICATION_INITIAL_SenderAccountBalance);
                    }
                    if (STATUS_REVERTED_RecieverAccount == false)
                    {
                        STATUS_REVERTED_RecieverAccount = await API_PROP_IRepository.TRANSACTION_SQL_ASYNC_UpdateAccountBalence(INPUT_RecieverAccount, VERIFICATION_INITIAL_RecieverAccountBalance);
                    }
                    if (STATUS_REVERTED_SenderAccount == true && STATUS_REVERTED_RecieverAccount == true)
                    {
                        break;
                    }
                    COUNTER_RevertTransaction++;
                }

                    // Runs if all else fails
                if (STATUS_REVERTED_SenderAccount == false || STATUS_REVERTED_RecieverAccount == false)
                {
                    API_PROP_Logger.LogError("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer !!REVERT!! ({0} , {1} , {2}) --> OUTPUT: !ERROR = UNABLE TO REVERT ORIGINAL STATE", INPUT_SenderAccount, INPUT_RecieverAccount, INPUT_ChangeAmount);
                }
                else
                {
                    API_PROP_Logger.LogTrace("EXECUTED: TRANSACTION_LOGIC_ASYNC_MoneyTransfer !!REVERT!! ({0} , {1} , {2}) --> OUTPUT: SUCCESSFULLY REVERT ORIGINAL STATE", INPUT_SenderAccount, INPUT_RecieverAccount, INPUT_ChangeAmount);
                }

                return -1;
            }
        }

    }
}
