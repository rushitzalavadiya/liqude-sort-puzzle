// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Purchasing;
//
// public class Purchaser : IStoreListener
// {
// 	private static IStoreController _mStoreController;
//
// 	private static IExtensionProvider _mStoreExtensionProvider;
//
// 	public bool Inilitized
// 	{
// 		get
// 		{
// 			if (_mStoreController != null)
// 			{
// 				return _mStoreExtensionProvider != null;
// 			}
// 			return false;
// 		}
// 	}
//
// 	public IEnumerable<string> ConsumableItems
// 	{
// 		get;
// 	}
//
// 	public IEnumerable<string> NonConsumableItems
// 	{
// 		get;
// 	}
//
// 	public event Action<string, bool> OnItemPurchased;
//
// 	public event Action<bool> RestorePurchased;
//
// 	public Purchaser(IEnumerable<string> consumableItems, IEnumerable<string> nonConsumableItems)
// 	{
// 		NonConsumableItems = nonConsumableItems;
// 		ConsumableItems = consumableItems;
// 		Init();
// 	}
//
// 	private void Init()
// 	{
// 		if (!Inilitized && _mStoreController == null)
// 		{
// 			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
// 			foreach (string consumableItem in ConsumableItems)
// 			{
// 				configurationBuilder.AddProduct(consumableItem, ProductType.Consumable);
// 			}
// 			foreach (string nonConsumableItem in NonConsumableItems)
// 			{
// 				configurationBuilder.AddProduct(nonConsumableItem, ProductType.NonConsumable);
// 			}
// 			UnityPurchasing.Initialize(this, configurationBuilder);
// 		}
// 	}
//
// 	public string GetPrice(string productId)
// 	{
// 		return GetProduct(productId)?.metadata.localizedPriceString;
// 	}
//
// 	public void BuyProduct(string productId, Action<bool> callback)
// 	{
// 		Action<string, bool> onPurchase = null;
// 		onPurchase = delegate(string id, bool success)
// 		{
// 			if (!(id != productId))
// 			{
// 				OnItemPurchased -= onPurchase;
// 				callback?.Invoke(success);
// 			}
// 		};
// 		OnItemPurchased += onPurchase;
// 		BuyProductID(productId);
// 	}
//
// 	public bool ItemAlreadyPurchased(string productId)
// 	{
// 		Product product = GetProduct(productId);
// 		if (product != null && product.definition.type != 0 && product.hasReceipt)
// 		{
// 			return true;
// 		}
// 		return false;
// 	}
//
// 	private void BuyProductID(string productId)
// 	{
// 		if (Inilitized)
// 		{
// 			Product product = _mStoreController.products.WithID(productId);
// 			if (product != null && product.availableToPurchase)
// 			{
// 				_mStoreController.InitiatePurchase(product);
// 			}
// 			else
// 			{
// 				this.OnItemPurchased?.Invoke(productId, arg2: false);
// 			}
// 		}
// 		else
// 		{
// 			this.OnItemPurchased?.Invoke(productId, arg2: false);
// 			Init();
// 		}
// 	}
//
// 	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
// 	{
// 		_mStoreController = controller;
// 		_mStoreExtensionProvider = extensions;
// 	}
//
// 	public void Restore()
// 	{
// 		_mStoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(delegate(bool success)
// 		{
// 			this.RestorePurchased?.Invoke(success);
// 		});
// 	}
//
// 	public Product GetProduct(string id)
// 	{
// 		return _mStoreController?.products.WithID(id);
// 	}
//
// 	public void OnInitializeFailed(InitializationFailureReason error)
// 	{
// 		UnityEngine.Debug.Log("OnInilized Failed");
// 	}
//
// 	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
// 	{
// 		UnityEngine.Debug.Log("Purchased Product:" + args.purchasedProduct.definition.id);
// 		this.OnItemPurchased?.Invoke(args.purchasedProduct.definition.id, arg2: true);
// 		return PurchaseProcessingResult.Complete;
// 	}
//
// 	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
// 	{
// 		this.OnItemPurchased?.Invoke(product.definition.id, arg2: false);
// 	}
// }
