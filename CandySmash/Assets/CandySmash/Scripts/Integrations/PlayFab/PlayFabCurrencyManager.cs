#if PLAYFAB
using UnityEngine;
using System.Collections;
using PlayFab.AdminModels;
using PlayFab.ClientModels;

using PlayFab;
using System.Collections.Generic;

public class PlayFabCurrencyManager {
    private static int currentBalance;

    public static void Init() {
        PlayFabManager.OnLoginEvent += GetBalance;
    }

    public static void IncBalance(int amount) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        PlayFab.ClientModels.AddUserVirtualCurrencyRequest request = new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "GC",
            Amount = amount
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => {
            Debug.Log(result.Balance);
        },
                        (error) => {
                            Debug.Log(error.ErrorMessage);
                        });
    }

    public static void DecBalance(int amount) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest request = new PlayFab.ClientModels.SubtractUserVirtualCurrencyRequest() {
            VirtualCurrency = "GC",
            Amount = amount
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(request, (result) => {
            Debug.Log(result.Balance);
        },
                        (error) => {
                            Debug.Log(error.ErrorMessage);
                        });
    }

    public static void SetBalance(int newbalance) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        GetBalance();
        PlayFab.ClientModels.AddUserVirtualCurrencyRequest request = new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "GC",
            Amount = newbalance - currentBalance
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => {
            Debug.Log(result.Balance);
        },
                        (error) => {
                            Debug.Log(error.ErrorMessage);
                        });

    }

    public static void GetBalance() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        PlayFab.ClientModels.AddUserVirtualCurrencyRequest request = new PlayFab.ClientModels.AddUserVirtualCurrencyRequest() {
            VirtualCurrency = "GC"
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => {
            currentBalance = result.Balance;
            InitScript.Gems = result.Balance;
        },
                    (error) => {
                        Debug.Log(error.ErrorMessage);
                        GetCurrencyList();
                    });

    }

    public static void GetCurrencyList() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        ListVirtualCurrencyTypesRequest request = new ListVirtualCurrencyTypesRequest() {

        };

        PlayFabAdminAPI.ListVirtualCurrencyTypes(request, (result) => {
            Debug.Log(result);
            if (result.VirtualCurrencies.Count == 0) {
                CreateCurrency();
            }
        },
                (error) => {
                    Debug.Log(error.ErrorMessage);
                });
    }




    static void CreateCurrency() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        Debug.Log("creating new currency...");

        List<VirtualCurrencyData> currencyList = new List<VirtualCurrencyData>();
        VirtualCurrencyData currency = new VirtualCurrencyData();
        currency.CurrencyCode = "GC";
        currency.DisplayName = "Coins";
        currency.InitialDeposit = InitScript.Gems;
        currencyList.Add(currency);

        AddVirtualCurrencyTypesRequest request = new AddVirtualCurrencyTypesRequest() {

            VirtualCurrencies = currencyList

        };

        PlayFabAdminAPI.AddVirtualCurrencyTypes(request, (result) => {
            Debug.Log(result);
            IncBalance(InitScript.Gems);
        },
            (error) => {
                Debug.Log(error.ErrorMessage);
            });
    }



}

#endif