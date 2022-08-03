using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
public class GalleryRegistView : View
{

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _imageUploadButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _registButton;
    [SerializeField] private InputField _contentText;
    [SerializeField] private TypeSelect _typeSelect;

    private RawImage img;
    public override void Initialized()
    {
        _soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ToggleBGM();
        });
        _exitButton.onClick.AddListener(() =>
        {
            UIManager.Show<INGAMEView>();
        });
        _imageUploadButton.onClick.AddListener(() =>
        {
            onClickImageUpload();
        });
        _cancelButton.onClick.AddListener(() =>
        {
            UIManager.Show<GalleryView>();
        });
        _registButton.onClick.AddListener(() =>
        {

        });
        _typeSelect.Init();
    }
    public void onClickImageUpload()
    {
        NativeGallery.GetImageFromGallery((file) =>
        {
            //선택햇음
            FileInfo selected = new FileInfo(file);

            //50메가 이상이면 저장 x
            if (selected.Length > 50000000)
            {
                return;
            }

            //파일 존재하면 불러오기
            if (!string.IsNullOrEmpty(file))
            {
                StartCoroutine(LoadImage(file));
            }
        });
    }

    IEnumerator LoadImage(string path)
    {
        yield return null;
        byte[] fileData = File.ReadAllBytes(path);//사진 데이터 다 받음
        string filename = Path.GetFileName(path).Split('.')[0];//확장자 자르기
        string savePath = Application.persistentDataPath + "/Image"; //한번 경로 지정하고 다시 이미지 지정안하게 저장 하기 눌럿으면 해당 경로에서 이미지 계속 불러옴
                                                                     //Debug.Log(Application.persistentDataPath )

        //아직 한번도 저장안햇다면
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        File.WriteAllBytes(savePath + filename + ".png", fileData); //만든 경로에 파일이 저장될것임
        var temp = File.ReadAllBytes(savePath + filename + ".png"); //저장된 파일을 바이트로 읽는다.

        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(temp);//바이트 배열을 2d로 변환시켜줌
        img.texture = tex;
    }
}
