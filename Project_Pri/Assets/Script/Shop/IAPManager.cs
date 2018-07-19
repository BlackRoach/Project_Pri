using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string kProductIDConsumable = "consumable";
    public static string kProductIDNonConsumable = "nonconsumable";
    public static string kProductIDSubscription = "subscription";

    // Apple App Store-specific product identifier for the subscription product.
    //private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";
    // 애플스토어 아이템 고유 식별자 코드
    private static string kProductNameAppleConsumable = "apple.consumable";
    private static string kProductNameAppleNonConsumable = "apple.nonconsumable";
    private static string kProductNameAppleSubscription = "apple.subscription";

    // Google Play Store-specific product identifier subscription product.
    //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
    // 구글 플레이스토어 아이템 고유 식별자 코드 (플레이스토어에서의 제품 ID)
    private static string kProductNameGooglePlayConsumable = "com.test.diamond10"; // 관리되는 아이템 (소모성 아이템)
    private static string kProductNameGooglePlayNonConsumable = "googleplay.nonconsumable"; // 플레이스토어에는 없는 것 같다. (비소모성 아이템)
    private static string kProductNameGooglePlaySubscription = "googleplay.subscription"; // (구독 아이템)

    // Use this for initialization
    void Start () {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // 초기화가 되어 있다면 초기화를 안하고 넘어간다.
        if (IsInitialized())
        {
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // 하단에 있는 것은 유니티 에디터/Window/Unity IAP>Receipt Validation Obfuscator 에서 지정해줘도 된다.
        // builder.Configure<IGooglePlayConfiguration>().SetPublicKey("~~");

        // 하단에 있는 것은 유니티 에디터/Window/Unity IAP>IAP Catalog 에서 지정해줘도 된다.
        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        // builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        builder.AddProduct(kProductIDConsumable, ProductType.Consumable, new IDs(){
            {kProductNameAppleConsumable, AppleAppStore.Name },
            {kProductNameGooglePlayConsumable, GooglePlay.Name },
        });


        // Continue adding the non-consumable product.
        // builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable, new IDs(){
            {kProductNameAppleNonConsumable, AppleAppStore.Name },
            {kProductNameGooglePlayNonConsumable, GooglePlay.Name },
        });
        // 아마존 같은 다른 서비스들도 추가할 수 있다.

        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 
        builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
            { kProductNameAppleSubscription, AppleAppStore.Name },
            { kProductNameGooglePlaySubscription, GooglePlay.Name },
        });

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyConsumable()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDConsumable);
    }


    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDNonConsumable);
    }


    public void BuySubscription()
    {
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        BuyProductID(kProductIDSubscription);
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>(); // 튜토리얼에는 <>가 없었다.
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action below, and ProcessPurchase if there are previously purchased products to restore.
            // 람다식
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    // 초기화가 잘 이루어졌을 때 호출이 됨. InitializePurchasing()에서 초기화를 함.
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    // 초기화중 에러가 생겼을시에 호출되는 함수
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    // 결제 성공시/완료시 호출되는 함수
    // 이 곳에서 결제가 성공되었을 때 골드 등을 추가해주면 됨.
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // 소모품 구매처리
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            // 상품 구매 성공이기 때문에 이곳에서 특정 값을 올려주거나 하면됨
            // ScoreManager.score += 100; // 상품이 구매되면 처리되는 부분. 이 부분을 자신의 게임에 맞게 수정
            GameObject.Find("Shop").GetComponent<Shop>().AddDiamond();
        }
        // 비소모품 구매처리
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        }
        // 구독 상품 구매 처리
        // Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    // 결제 실패시 호출되는 함수
    // 결제창에서 취소를 눌러도 이 OnPurchaseFailed() 함수가 호출된다.
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
