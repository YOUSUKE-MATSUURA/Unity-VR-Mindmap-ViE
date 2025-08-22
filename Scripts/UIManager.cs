using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    
    public class UIManager : MonoBehaviour
    {
        //エラーメッセージ処理の表示内容を格納するために、[SerializeField]属性のフィールドdisplayMessageを宣言します。
        //スクリプトをアタッチ後にUnityエディターによりフィールドdisplayMessageとテキストボックス[Message]を関連付けます。
        [SerializeField] private TextMeshProUGUI displayMessage;
        
        //次に、回転させるゲームオブジェクトを格納するために、フィールドtargetObjectを宣言します。
        [SerializeField]GameObject targetObject;
        
        //ボタンなどのUIオブジェクトを格納するために、フィールドbuttonForStartなどを宣言します。
        [SerializeField]Button buttonForStart;
        [SerializeField]Slider sliderForSpeed;
        
        //型名はDropdown型、InputField型ではなく、TMP_Dropdown型、TMP_InputField型であることに留意します。
        [SerializeField]TMP_Dropdown dropdownForSpeedMode;
        [SerializeField]Toggle toggleForReverse;
        [SerializeField]TMP_InputField inputFieldForSpeed;

        //このクラスの処理に必要なコンポーネントなどの前準備ができているか否かを表すフラグisReady（bool型）を定義します。
        //また、オブジェクトの回転処理の開始を表すフラグhasStatedを定義します。
        bool isReady;
        bool hasStarted;

        //オブジェクトの回転速度の可変範囲は「Normal Mode」(0~90度/秒)と「High Speed Mode」（0~720度/秒）の2種類とします。
        //回転モード名と最大速度をタプルとして１つにまとめ、それを定数のリストSpeedModeDataとして定義します。
        //リスト内の要素も定数にするため、IReadOnlyList型を用います。
        static readonly IReadOnlyList<(string modeName, float maxSpeed)> SpeedModeData = new[]
        {
            ("Normal Mode", 90f),
            ("High Speed Mode", 720f),
        };

        float rotationSpeed = SpeedModeData[0].maxSpeed;
        int rotationSign = 1;

        private void Awake()
        {
            //フィールドdisplayMessageの設定値に不備がある場合は、アプリを終了します。
            if (displayMessage is null)
            {
                Application.Quit();
            }

            //フィールドtargetObjectおよびUIオブジェクトのフィールドの設定値に不備がある場合は、フィールドisReadyにfalseを格納し、
            //エラーメッセージをパネルに表示し、処理を中断します。
            if (targetObject is null || buttonForStart is null || sliderForSpeed is null ||
               dropdownForSpeedMode is null || toggleForReverse is null || inputFieldForSpeed is null)
            {
                isReady = false;
                var errorMessage = "#targetObject or #UI Objects";
                displayMessage.text = $"{GetSourceFileName()}\r\nError: {errorMessage}";
                return;
            }

            isReady = true;
        }

        private void OnEnable()
        {
            if(!isReady){return;}
            //ボタンがクリックされたときに呼ばれるイベントリスナーを登録します。
            buttonForStart.onClick.AddListener(OnButtonClicked);

            //スライダーの可変範囲などを設定し、スライダーのハンドルが操作されたときに呼ばれるイベントリスナーを登録します。
            sliderForSpeed.maxValue = SpeedModeData[0].maxSpeed;
            sliderForSpeed.minValue = 0;
            sliderForSpeed.value = rotationSpeed;
            sliderForSpeed.onValueChanged.AddListener(OnSliderValueChanged);
            
            //ドロップダウンの選択項目のリストなどを設定し、ドロップダウンが操作されたときに呼ばれるイベントリスナーを登録します。
            dropdownForSpeedMode.ClearOptions();
            foreach (var (modeName, _) in SpeedModeData)
            {
                dropdownForSpeedMode.options.Add(new TMP_Dropdown.OptionData(modeName));
            }
            //dropdownForSpeedMode.value = dropdownForSpeedMode.RefreshShownValue();
            dropdownForSpeedMode.onValueChanged.AddListener(OnDropdownValueChanged);
            
            //トグルの初期化を行い、トグルが操作されたときに呼ばれるイベントリスナーを登録します。
            toggleForReverse.isOn = false;
            toggleForReverse.onValueChanged.AddListener(OnToggleValueChanged);

            //入力フィールドの入力文字種の制限を設定します。そして、入力フィールドがフォーカスされたとき（onSelect）,
            //および入力が完了したとき（onEndEdit）に呼ばれるイベントリスナーを登録します。
            inputFieldForSpeed.contentType = TMP_InputField.ContentType.DecimalNumber;
            inputFieldForSpeed.onSelect.AddListener(OnInputFieldEndEdit);
            inputFieldForSpeed.onEndEdit.AddListener(OnInputFieldEndEdit);

            //ボタン以外のUIオブジェクトのインタラクションを不許可にするため、フィールドhasStatedにtrueを格納してから、
            //メソッドOnButtonClickedを呼び出します。
            hasStarted = true;
            OnButtonClicked();
        }

        private void OnDisable()
        {
            if(!isReady){return;}
            
            //メソッドOnEnableで登録したイベントリスナーを解除します。
            buttonForStart.onClick.RemoveListener(OnButtonClicked);
            sliderForSpeed.onValueChanged.RemoveListener(OnSliderValueChanged);
            dropdownForSpeedMode.onValueChanged.RemoveListener(OnDropdownValueChanged);
            toggleForReverse.onValueChanged.RemoveListener(OnToggleValueChanged);
            inputFieldForSpeed.onSelect.RemoveListener(OnInputFieldEndEdit);
        }

        void Update()
        {
            if(!isReady || !hasStarted){return;}

            //フレームが更新されるたびに回転角を算出し、オブジェクトを回転させます。そして、その回転速度をパネルに表示します。
            var angularVelocity = rotationSign * rotationSpeed * Vector3.up;
            targetObject.transform.Rotate(angularVelocity * Time.deltaTime);
            displayMessage.text = $"Rtation Speed: {rotationSign * rotationSpeed:F1}[deg/s]";
        }

        //メソッドOnButtonClickedは、ボタンがクリックされたときに呼ばれるイベントリスナーです。
        //呼ばれるたびにフィールドhasStarted(bool型)の値を反転させます。
        //そして、UIオブジェクトのインタラクションの状態および回転するオブジェクトのアクティブ状態を設定します。
        void OnButtonClicked()
        {
            hasStarted = !hasStarted;
            targetObject.SetActive(hasStarted);
            sliderForSpeed.interactable = hasStarted;
            dropdownForSpeedMode.interactable = hasStarted;
            toggleForReverse.interactable = hasStarted;
            inputFieldForSpeed.interactable = hasStarted;
            displayMessage.text = "";
        }

        //メソッドOnSliderValueChangedは、スライダーが操作されたときに呼ばれるイベントリスナーです。
        //スライダーの位置に応じた値をフィールドrotationSpeedに格納します。
        void OnSliderValueChanged(float value) => rotationSpeed = value;
        
        //メソッドOnDropdownValueChangedは、ドロップダウンが操作されたときに呼ばれるイベントリスナーです。
        //選択された回転速度モードに応じて、リストSpeedModeDataの要素の値（指定されたモードの最大速度）を得て、
        //スライダーのプロパティmaxValueに格納します。
        void OnDropdownValueChanged(int index) => sliderForSpeed.maxValue = SpeedModeData[index].maxSpeed;

        //メソッドOnToggleValueChangedは、トルグが操作されたときに呼ばれるイベントリスナーです。
        //トルグがチェックされたときに「-1」を、そうでないときは「1」をフィールドrotationSignに格納します。
        //なお、回転の速さにrotationSignを乗算することで、回転の向き（時計回りor反時計回り）を定めています。
        void OnToggleValueChanged(bool inOn) => rotationSign = inOn ? -1 : 1;
        
        //メソッドOnInputFieldSelectは、入力フィールドがフォーカスされたときに呼ばれるイベントリスナーです
        //フォーカスされたときに、以前の入力値を削除し初期化するために、入力フィールドのテキストの値を空文字にします。
        void OnInputFieldSelect(string text) => inputFieldForSpeed.text = "";

        
        //メソッドOnInputFieldEndEditは、入力フィールドへの入力が完了したときに呼ばれるイベントリスナーです。
        //引数textから得られた入力データ（string型）をfloat.TryParseにより実数（float型）に変換します。
        //変換に成功した場合はその値をフィールドrotationSpeedに格納し、そうでない場合は以前の値を維持します。
        void OnInputFieldEndEdit(string text) =>
            rotationSpeed = float.TryParse(text, out var num) ? num : rotationSpeed;
    }
}
