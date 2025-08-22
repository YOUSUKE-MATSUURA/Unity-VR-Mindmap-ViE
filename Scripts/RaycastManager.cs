using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using static UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    public class RaycastManager : MonoBehaviour
    {
        //右手部コントローラーのレイのインストラクターを格納するために、
        //フィールドrightRayを宣言します。
        [SerializeField] private XRRayInteractor rightRay;
        
        //左手部コントローラーのレイのインストラクターを格納するために、
        //フィールドleftRayを宣言します。
        [SerializeField] private XRRayInteractor leftRay;
        
        //また、インタラクティブを選択した際に発する効果音の音源を格納するために、
        //フィールドsoundEffectを宣言します。
        [SerializeField] private AudioClip soundEffect;
        
        
        //このクラスの処理に必要なコンポーネントなどの前準備ができているか否かを表す
        //フラグisReady(bool型)を定義します。
        private bool isReady;

        private void Awake()
        {
            //フィールドrightRayおよびsoundEffectの設定値に不備がある場合は、
            //フィールドisReadyにfalseを格納し、処理を中断します。
            if (rightRay is null || leftRay is null || soundEffect is null)
            {
                isReady = false;
                return;
            }

            isReady = true;
        }

        private void OnEnable()
        {
            //フィールド設定などに不備がある（フラグisReadyが真ではない）場合、
            //何もせずにメソッドを中断します。
            if(!isReady){return;}
            
            //XRRayInteractor型のフィールドrightRayとleftRayのプロパティの値を設定します。
            //ここではuseForceGrabをfalseに設定し、選択時にインタラクタブルを手元に引き寄せないようにします。
            //また、hitClosestOnlyをtrueに設定し、レイが複数のオブジェクトにヒットしても、
            //最も手前にあるオブジェクトのみ操作対象とします。
            rightRay.useForceGrab = false;
            leftRay.useForceGrab = false;
            
            rightRay.hitClosestOnly = true;
            leftRay.hitClosestOnly = true;
            
            //ホバリングの開始時・終了時および選択の開始時・終了時のイベントが発生した際のイベントリスナーを登録します。
             rightRay.hoverEntered.AddListener(OnHoverEntered);
            
             rightRay.hoverExited.AddListener(OnHoverExited);
            
             rightRay.selectEntered.AddListener(OnSelectEntered);
            
             rightRay.selectExited.AddListener(OnSelectExited);
            
            //選択した際にコントローラーが振動するように設定します。
            rightRay.playHapticsOnSelectEntered = true;
            leftRay.playHapticsOnSelectEntered = true;

            rightRay.hapticSelectEnterIntensity = 1f;
            leftRay.hapticSelectEnterIntensity = 1f;
            
            rightRay.hapticSelectEnterDuration = 0.1f;
            leftRay.hapticSelectEnterDuration = 0.1f;
            
            //選択した際に効果音を発するように設定します。
            rightRay.playAudioClipOnSelectEntered = true;
            leftRay.playAudioClipOnSelectEntered = true;
            rightRay.audioClipForOnSelectEntered = soundEffect;
            leftRay.audioClipForOnSelectEntered = soundEffect;
        }
        
        //OnEnableで登録したイベントリスナーを解除します。
        private void OnDisable()
        {
            if(!isReady){return;}
            
            rightRay.hoverEntered.RemoveListener(OnHoverEntered);
            
            rightRay.hoverExited.RemoveListener(OnHoverExited);
            
            rightRay.selectEntered.RemoveListener(OnSelectEntered);
            
            rightRay.selectExited.RemoveListener(OnSelectExited);
        }
        
        //メソッドDisplayInteractionsはインタラクションの局面（状態）をパネルに表示します。
        //引数argsはイベントリスナーの引数をそのまま受け取ります。
        //イベントリスナーの引数の型（HoverEnterEventArgs型など）の基底クラスはBaseInteractionEventArgs型です。
        //これを制約条件にしたジェネリックメソッドとして定義します。
        void DisplayInteractions<T>(T args, string EventListenerName) where T : BaseInteractionEventArgs
        {

            //イベントリスナー名をパネルに表示します。
            //displayMessage.text = $"{EventListenerName}\r\n";


            //レイのインストラクターがホバリングしている場合は、引数からインタラクタブル名を取得し、
            //レイインタラクター名とインタラクタブル名をパネルに表示します。
            if (rightRay.hasHover)
            {
                var grabInteractable = args.interactableObject as XRGrabInteractable;
                var grabInteractableName = grabInteractable != null ? grabInteractable.name : "UnKnown";
               // displayMessage.text +=
                   // $">Interactor: {rightRay.name}\r\n" + $">Interactable: {grabInteractableName}\r\n";
            }

            //レイインタラクターがインタラクタブルを選択している場合は、
            //レイがヒットした位置の座標を求め、その値をパネルに表示します。
            if (rightRay.hasSelection && rightRay.TryGetCurrent3DRaycastHit(out var hit))
            {
                //displayMessage.text += $">Hit Position: {hit.point}\r\n";
            }
        }
        
        //メソッドOnHoverEnteredとOnHoverExitedは、
        //レイのインタラクターがホバリングを開始および終了した際に呼ばれるイベントリスナーです。
        //メソッドDisplayInteractionsにより、インタラクションの局面（状態）をパネルに表示します。
        void OnHoverEntered(HoverEnterEventArgs args) => DisplayInteractions(args, GetCallerMember());

        void OnHoverExited(HoverExitEventArgs args) => DisplayInteractions(args, GetCallerMember());

        //同様に、メソッドOnSelectEnteredとOnSelectExitedはレイインタラクターが選択を開始および終了した際に呼ばれるイベントリスナーです。
        void OnSelectEntered(SelectEnterEventArgs args) => DisplayInteractions(args, GetCallerMember());

        void OnSelectExited(SelectExitEventArgs args) => DisplayInteractions(args, GetCallerMember());
    }

}
