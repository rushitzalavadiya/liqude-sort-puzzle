// using UnityEngine;
// using UnityEngine.EventSystems;
//
// public class NoAdsButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
// {
// 	private void Awake()
// 	{
// 		base.gameObject.SetActive(ResourceManager.EnableAds);
// 	}
//
// 	private void OnEnable()
// 	{
// 		ResourceManager.ProductPurchased += ResourceManagerOnProductPurchased;
// 	}
//
// 	private void OnDisable()
// 	{
// 		ResourceManager.ProductPurchased -= ResourceManagerOnProductPurchased;
// 	}
//
// 	private void ResourceManagerOnProductPurchased(string productId)
// 	{
// 		base.gameObject.SetActive(ResourceManager.EnableAds);
// 	}
//
// 	public void OnPointerClick(PointerEventData eventData)
// 	{
// 		ResourceManager.PurchaseNoAds(delegate
// 		{
// 		});
// 	}
// }
