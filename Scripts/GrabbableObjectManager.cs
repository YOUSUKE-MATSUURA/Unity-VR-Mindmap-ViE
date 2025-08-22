using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using static UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GrabbableObjectManager : MonoBehaviour
    {
        //エラーメッセージや処理の表示内容を格納するために、
        //[SerializeField]属性のフィールドdisplayMessageを宣言します。
        //スクリプトをアタッチ後にUnityエディターにより
        //フィールドdisplayMessageとテキストボックス[Message2]を関連付けます。
        //[SerializeField]TextMeshProUGUI displayMessage;

        //このクラスの処理に必要なコンポーネントなどの前準備ができているか否かを表す
        //フラグisReady（bool型）を定義します。
        bool isReady;
        
        //このスクリプトがアタッチされているオブジェクトの
        //[XRGrabInteractable]コンポーネントの情報を格納するために、
        //フィールドgrabInteractableを宣言します。
        XRGrabInteractable grabInteractable;
        
        //オブジェクト[GrabbableCube]の色を変化させるためのレンダラーを格納するための
        //フィールドmeshRendererと、オブジェクト[GrabbableCube]の元の色を格納するためのフィールドnormalColor,
        //Activate時に使用する色を格納するための定数ColorOnActivatedを宣言します。
        MeshRenderer meshRenderer;
        Color normalColor;
        static readonly Color ColorOnActivated = Color.red;

        void Awake()
        {
            //フィールドdisplayMessageの設定値に不備がある場合は、アプリを終了します。
            //if (displayMessage is null)
            //{
                //Application.Quit();
            //}

            //このスクリプトがアタッチされているオブジェクト[GrabbableCube]の
            //コンポーネント[XRGrabInteractable]からインタラクタブルの情報を得て、フィールドgrabInteractableに格納します。
            //また、オブジェクト[GrabbableCube]の色を変更するために、このオブジェクトのレンダラーをあらかじめ取得し、
            //フィールドmeshRendererに格納します。
            grabInteractable = GetComponent<XRGrabInteractable>();
            meshRenderer = GetComponent<MeshRenderer>();
            
            //フィールドgrabInteractableおよびmeshRendererの設定値に不備がある場合は、フィールドisReadyにfalseを格納し、
            //エラーメッセージをパネルに表示し、処理を中断します。
            if (grabInteractable is null || meshRenderer is null || !meshRenderer.enabled)
            {
                isReady = false;
                //var errorMessage = "#grabInteractable or #meshRenderer";
                //displayMessage.text = $"{GetSourceFileName()}\r\nError: {errorMessage}";
                return;
            }

            isReady = true;
        }

        void OnEnable()
        {
            if(!isReady){return;}
            
            //オブジェクト[GrabbableCube]の元の色を得て、フィールドnormalColorに格納します。
            normalColor = meshRenderer.material.color;
            
            
            //grabInteractableの各イベントにおけるイベントリスナーを登録します。
            //grabInteractable.selectEntered.AddListener(OnSelectEntered);
            
            grabInteractable.selectExited.AddListener(OnSelectExited);
            
            grabInteractable.activated.AddListener(OnActivated);
            
            grabInteractable.deactivated.AddListener(OnDeactivated);
        }

        void OnDisable()
        {
            if(!isReady){return;}
            
            //メソッドOnEnableで登録したイベントリスナーを解除します。
            //grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
            
            grabInteractable.activated.RemoveListener(OnActivated);
            
            grabInteractable.deactivated.RemoveListener(OnDeactivated);
        }

        //メソッドOnSelectEnteredは、アクション[Select]が起動したときに呼ばれるイベントリスナーです。イベントリスナー名をパネルに表示します。
        //void OnSelectEntered(SelectEnterEventArgs args) => displayMessage.text = $"{GetCallerMember()}\r\n";


        //メソッドOnSelectExitedは、アクション[Select]が停止した時に呼ばれるイベントリスナーです。
        //イベントリスナー名をパネルに表示し、オブジェクトの色を元の色（normalColor）に戻します。
        void OnSelectExited(SelectExitEventArgs args)
        {
            //displayMessage.text = $"{GetCallerMember()}\r\n";
            meshRenderer.material.color = normalColor;
        }

        //メソッドOnActivatedは、アクション[Activate]が起動したときに呼ばれるイベントリスナーです。
        //イベントリスナー名をパネルに表示し、オブジェクトの色をColorOnActivated（赤色）に変更します。
        void OnActivated(ActivateEventArgs args)
        {
           // displayMessage.text = $"{GetCallerMember()}\r\n";
            meshRenderer.material.color = ColorOnActivated;
        }

        
        //メソッドOnDeactivatedは、アクション[Activate]が停止した時に呼ばれるイベントリスナーです。
        //メソッドOnSelectExitedと同様な処理を行います。
        void OnDeactivated(DeactivateEventArgs args)
        {
            //displayMessage.text = $"{GetCallerMember()}\r\n";
            meshRenderer.material.color = normalColor;
        }
    }
}
