using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// api.getAuthToken(); ответ в getAuthTokenCallback
/// api.userFriends(); ответ в userFriendsCallback
/// api.userSocialFriends(); ответ в userSocialFriendsCallback
/// api.userInfo(); ответ в userInfoCallback
/// api.userProfile(); ответ в userProfileCallback
/// </summary>
public class EasyVKSDK : MonoBehaviour
{
    private VKPlayWeb api;

    public static EasyVKSDK Singleton { get; private set; }

    private void Start()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }

        api = VKPlayWeb.instance;
        int gmrId = 52812891; // <<< заменить на свой!!!! См. Кабинет -> Системные свойства -> ID Игры (GMRID)

        // important callbacks
        api.apiInitCallback += OnApiInit;
        api.getLoginStatusCallback += OnGetLoginStatus;
        api.registerUserCallback += OnRegisterUser;
        api.adsCallback += OnAdsCallback;

        // callbacks
        api.userFriendsCallback += OnUserFriends;
        api.userSocialFriendsCallback += OnUserSocialFriendsCallback;
        api.userInfoCallback += OnUserInfoCallback;
        api.userProfileCallback += OnUserProfileCallback;
        api.confirmWindowClosedCallback += OnconfirmWindowClosedCallback;
        api.getAuthTokenCallback += OnGetAuthTokenCallback;

        Debug.Log($"before api init, gmr id = {gmrId}");
        api.init(gmrId);

        Debug.Log("api init called, wait for apiInitCallback...");
        // после этого ждём вызов apiInitCallback... будет он сильно позже, не сразу
    }

    private void OnGetAuthTokenCallback(object sender, GetAuthTokenCallbackData args)
    {
        Debug.Log(args);

        if (args.status == "ok")
        {
            Debug.Log($"token data, uid={args.uid}, token={args.hash}");
        }
    }

    private void OnconfirmWindowClosedCallback(object sender, ConfirmWindowClosedCallbackData args)
    {
        Debug.Log(args);
    }

    public void ShowAds()
    {
        VKPlayWebAdsConfig adsConfig = new VKPlayWebAdsConfig()
        {
            interstitial = false
        };

        api.showAds(adsConfig);
    }

    private void OnAdsCallback(object sender, AdsCallbackData args)
    {
        Debug.Log(args);

        // здесь нет поля .status, нужно использовать .code
        // См. https://documentation.vkplay.ru/f2p_vkp/f2pb_adbreak_vkp
        if (args.type == "adError")
        {
            // что-то очень плохо с рекламой!
        }
    }

    private void OnUserProfileCallback(object sender, UserProfileCallbackData args)
    {
        Debug.Log(args);

        if (args.status == "ok")
        {
            Debug.Log($"Hello {args.nick}!");
        }
    }

    private void OnUserInfoCallback(object sender, UserInfoCallbackData args)
    {
        Debug.Log(args);

        if (args.status == "ok")
        {
            Debug.Log($"uid={args.uid}, hash={args.hash}");
        }
    }

    private void OnUserSocialFriendsCallback(object sender, UserSocialFriendsCallbackData args)
    {
        Debug.Log(args);
    }

    private void OnUserFriends(object sender, UserFriendsCallbackData args)
    {
        Debug.Log(args);
    }

    private void OnRegisterUser(object sender, UserInfoCallbackData args)
    {
        Debug.Log(args);

        if (args.status != "ok")
        {
            // Что-то пошло не так...? Дать пользователю повторить регу.
            return;
        }

        /*
        https://documentation.vkplay.ru/f2p_vkp/f2pb_js_vkp#registerUser
            Чтобы все параметры после вызова метода registerUser, гарантированно, передались в iframe с игрой,
            требуется перезагрузить страницу с игрой, используя метод reloadWindow.
            Сделать это можно, например,
            сделав следующий вызов сразу после вызова метода registerUser:
        */

        api.reloadWindow();
    }

    private void OnGetLoginStatus(object sender, GetLoginStatusCallbackData args)
    {
        if (args.loginStatus == 0) // нужна авторизация
        {
            api.authUser();
        }
        else if (args.loginStatus == 1) // нужна регистрация
        {
            api.registerUser();
        }
        else if (args.loginStatus == 2 || args.loginStatus == 3) // всё норм
        {
            // ShowAds();
        }
    }

    private void OnApiInit(object sender, ApiInitCallbackData args)
    {
        if (args.status == "ok")
        {
            // Получить инфу о пользователе, она придёт в getLoginStatusCallback...
            api.getLoginStatus();
        }
        else
        {
            Debug.Log($"api init fail, reason = {args.errmsg}");
        }
    }
}
