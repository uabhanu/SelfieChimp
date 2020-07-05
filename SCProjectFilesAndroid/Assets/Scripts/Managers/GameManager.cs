﻿using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    AudioSource _musicSource;
	Image _adsAcceptButtonImage , _adsCancelButtonImage , _backToLandLoseMenuImage , _backToLandWinMenuImage , _backToLandWithSuperMenuImage , _continueButtonImage , _exitButtonImage , _firstTimePlayTutorialMenuImage;
    Image _firstTimePlayTutorialOKButtonImage , _iapCartButtonImage , _pauseMenuImage , _playButtonImage , _quitButtonImage , _quitAcceptButtonImage , _quitCancelButtonImage , _quitMenuImage , _resumeButtonImage;
    int _chimpionshipsCount , _currentChimpion;
    LandPuss _landChimp;
	SoundManager _soundManager;
	Text _adsText , _backToLandLoseText , _backToLandWinText , _backToLandWithSuperText , _firstTimePlayTutorialText , _quitText;

    static Animator _swipeDownHandAnimator , _swipeUpHandAnimator;
    static Image _swipeDownHandImage , _swipeUpHandImage , _swipeHandOKButtonImage , _swipeHandPanelImage;
    static int _firstTimeJump = 0 , _firstTimeSlide = 0;

	[SerializeField] bool _bSelfieFlashEnabled , _bVersionCodeDisplayEnabled;
    [SerializeField] GameObject _iapCartMenuObj;
    [SerializeField] Image _chimpionshipBeltMenuImage , _chimpionshipOKButtonImage , _landLevelButtonImage , _waterLevelButtonImage;
    [SerializeField] Sprite[] _chimpionshipBeltSprites;
    [SerializeField] string _chimpionAchievementID , _selfieAchievementID , _selfieLegendAchievementID , _undisputedChimpionAchievementID;
    [SerializeField] Text _chimpionshipBeltText , _memoryLeakTestText , _versionCodeText;

    public static bool b_isFirstTimeTutorialTestingMode , b_isMemoryLeakTestingMode , b_isUnityEditorTestingMode , b_quitButtonTapped;
    public static Button m_chimpionshipBeltButton , m_muteButton , m_pauseButton , m_unmuteButton;
    public static GameObject m_pauseMenuObj , m_uiButtonsTutorialMenuObj;
    public static Image m_adsMenuImage , m_arrow01Image , m_arrow02Image , m_arrow03Image , m_arrow04Image , m_chimpionshipBeltButtonImage , m_muteButtonImage , m_nextButtonImage , m_pauseButtonImage;
    public static Image m_polaroidImage , m_selfieButtonImage , m_selfiePanelImage , m_uiButtonsTutorialMenuImage , m_unmuteButtonImage;
    public static int m_currentScene , m_firstTimeIAPTutorialAppeared , m_firstTimeUIButtonsTutorial , m_firstTimeWaterLevelTutorial , m_playerMutedSounds;
    public static Text m_chimpionshipBeltButtonTutorialText , m_leaderboardButtonTutorialText , m_muteUnmuteButtonTutorialText , m_pauseButtonTutorialText , m_polaroidsCountText;

    public Text m_highScoreLabelText , m_highScoreValueText;

    void Start()
	{
        //b_isFirstTimeTutorialTestingMode = true; //This is for testing only
        //b_isMemoryLeakTestingMode = true; //This is for testing only
        //b_isUnityEditorTestingMode = true; //This is for testing only
        Advertisement.Initialize("3696337");
        GetBhanuObjects();
    }

    public void Ads()
    {
        m_adsMenuImage.enabled = true;
        _adsAcceptButtonImage.enabled = true;
        _adsCancelButtonImage.enabled = true;
        _adsText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
        m_highScoreLabelText.enabled = false;
        m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_polaroidsCountText.enabled = false;
        m_polaroidImage.enabled = false;
		m_selfieButtonImage.enabled = false;
        m_unmuteButtonImage.enabled = false;
		Time.timeScale = 0;
    }

    public void AdsAcceptButton()
    {
        m_adsMenuImage.enabled = false;
        _adsAcceptButtonImage.enabled = false;
        _adsCancelButtonImage.enabled = false;
        _adsText.enabled = false;
        AdsShow();
    }

    public void AdsCancelButton()
    {
        BhanuPrefs.DeleteScore();
        ScoreManager.m_supersCount = 0;
        BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    void AdsShow()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdsWatchResult;
        Advertisement.Show("rewardedVideo" , options);
    }

    void AdsWatchResult(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            Time.timeScale = 1;
            SceneManager.LoadScene(m_currentScene);
        }

        else if(result == ShowResult.Skipped)
        {
            //Debug.LogWarning("Video was skipped - Do NOT reward the player");
            BhanuPrefs.DeleteScore();
        }

        else if(result == ShowResult.Failed)
        {
            //Debug.LogError("Video failed to show");
            BhanuPrefs.DeleteScore();
        }
    }

    public void BackToLandLoseMenu()
    {
        _backToLandLoseMenuImage.enabled = true;
        _backToLandLoseText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		_continueButtonImage.enabled = true;
		m_highScoreLabelText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_polaroidImage.enabled = false;
        m_polaroidsCountText.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWinMenu()
    {
        _backToLandWinMenuImage.enabled = true;
        _backToLandWinText.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		_continueButtonImage.enabled = true;
		m_highScoreLabelText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_polaroidImage.enabled = false;
        m_polaroidsCountText.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void BackToLandWithSuperMenu()
    {
        _backToLandWithSuperMenuImage.enabled = true;
        _backToLandWithSuperText.enabled = true;
        _continueButtonImage.enabled = true;
        m_chimpionshipBeltButtonImage.enabled = false;
		m_highScoreLabelText.enabled = false;
		m_highScoreValueText.enabled = false;
        m_muteButtonImage.enabled = false;
        m_pauseButtonImage.enabled = false;
        m_polaroidImage.enabled = false;
        m_polaroidsCountText.enabled = false;
        m_unmuteButtonImage.enabled = false;
        Time.timeScale = 0;
    }

    public void ContinueButton()
    {
		BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene("LandRunner");
    }

    void EndFlash()
	{
		m_selfiePanelImage.enabled = false;
	}

    public void ExitButton()
	{
        BhanuPrefs.DeleteScore();
        SceneManager.LoadScene("MainMenu");
        //TODO Exit Confirm Menu warning about 0 High Score
    }

    public void FirstTimeJumpTutorial()
    {
        if(_firstTimeJump == 0)
        {
            m_chimpionshipBeltButtonImage.enabled = false;
            m_highScoreLabelText.enabled = false;
            m_highScoreValueText.enabled = false;
            m_muteButtonImage.enabled = false;
            m_pauseButtonImage.enabled = false;
            _swipeUpHandAnimator.enabled = true;
            _swipeUpHandImage.enabled = true;
            _swipeHandOKButtonImage.enabled = true;
            _swipeHandPanelImage.enabled = true;
            m_unmuteButtonImage.enabled = false;
            _firstTimeJump++;
            BhanuPrefs.SetFirstTimeJumpTutorialStatus(_firstTimeJump);
            Time.timeScale = 0;
        }
    }

    public void FirstTimeSlideTutorial()
    {
        if(_firstTimeSlide == 0)
        {
            m_chimpionshipBeltButtonImage.enabled = false;
            m_highScoreLabelText.enabled = false;
            m_highScoreValueText.enabled = false;
            m_muteButtonImage.enabled = false;
            m_pauseButtonImage.enabled = false;
            _swipeDownHandAnimator.enabled = true;
            _swipeDownHandImage.enabled = true;
            _swipeHandOKButtonImage.enabled = true;
            _swipeHandPanelImage.enabled = true;
            m_unmuteButtonImage.enabled = false;
            _firstTimeSlide++;
            BhanuPrefs.SetFirstTimeSlideTutorialStatus(_firstTimeSlide);
            Time.timeScale = 0;
        }
    }

    public void FirstTimeWaterLevelOKButton()
    {
        m_chimpionshipBeltButtonImage.enabled = true;
        _firstTimePlayTutorialMenuImage.enabled = false;
        _firstTimePlayTutorialOKButtonImage.enabled = false;
        _firstTimePlayTutorialText.enabled = false;
        m_highScoreLabelText.enabled = true;
        m_highScoreValueText.enabled = true;
        m_pauseButtonImage.enabled = true;
        m_polaroidsCountText.enabled = true;
        m_polaroidImage.enabled = true;
        m_firstTimeWaterLevelTutorial = 1;
        BhanuPrefs.SetFirstTimeWaterLevelTutorialStatus(m_firstTimeWaterLevelTutorial);
        Time.timeScale = 1;
    }

    void GetBhanuObjects()
    {
        m_currentScene = SceneManager.GetActiveScene().buildIndex;
        m_muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
        m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
        m_unmuteButton = GameObject.Find("UnmuteButton").GetComponent<Button>();
        m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

        if(!b_isFirstTimeTutorialTestingMode)
        {
            _chimpionshipsCount = BhanuPrefs.GetChimpionshipsCount();
            m_firstTimeIAPTutorialAppeared = BhanuPrefs.GetFirstTimeIAPTutorialStatus();
            _firstTimeJump = BhanuPrefs.GetFirstTimeJumpTutorialStatus();
            _firstTimeSlide = BhanuPrefs.GetFirstTimeSlideTutorialStatus();
            m_firstTimeUIButtonsTutorial = BhanuPrefs.GetFirstTimeUIButtonsTutorialStatus();
            m_firstTimeWaterLevelTutorial = BhanuPrefs.GetFirstTimeWaterLevelTutorialStatus();
            m_playerMutedSounds = BhanuPrefs.GetSoundsStatus();
            Time.timeScale = 1;
        }
        else
        {
            BhanuPrefs.DeleteAll();
        }

        if(m_currentScene == 0)
        {
            _playButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
            _quitText = GameObject.Find("QuitText").GetComponent<Text>();
            _quitButtonImage = GameObject.Find("QuitButton").GetComponent<Image>();
            _quitAcceptButtonImage = GameObject.Find("QuitAcceptButton").GetComponent<Image>();
            _quitCancelButtonImage = GameObject.Find("QuitCancelButton").GetComponent<Image>();
            _quitMenuImage = GameObject.Find("QuitMenu").GetComponent<Image>();

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    m_unmuteButtonImage.enabled = true;
                }
            }
            else
            {
                Invoke("GetBhanuObjects" , 0.5f);
            }

            if(_bVersionCodeDisplayEnabled)
            {
                _versionCodeText.enabled = true;
            }
        }

        else if(m_currentScene == 1)
        {
            _adsText = GameObject.Find("AdsText").GetComponent<Text>();
            _adsAcceptButtonImage = GameObject.Find("AdsAcceptButton").GetComponent<Image>();
            _adsCancelButtonImage = GameObject.Find("AdsCancelButton").GetComponent<Image>();
            m_adsMenuImage = GameObject.Find("AdsMenu").GetComponent<Image>();
            m_arrow01Image = GameObject.Find("Arrow01").GetComponent<Image>();
            m_arrow02Image = GameObject.Find("Arrow02").GetComponent<Image>();
            m_arrow03Image = GameObject.Find("Arrow03").GetComponent<Image>();
            m_arrow04Image = GameObject.Find("Arrow04").GetComponent<Image>();
            m_chimpionshipBeltButton = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Button>();
            m_chimpionshipBeltButtonImage = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Image>();
            m_chimpionshipBeltButtonTutorialText = GameObject.Find("ChimpionBeltButtonTutorialText").GetComponent<Text>();
            _exitButtonImage = GameObject.Find("ExitButton").GetComponent<Image>();
            _iapCartButtonImage = GameObject.Find("IAPCartButton").GetComponent<Image>();
            _landChimp = GameObject.Find("LandPuss").GetComponent<LandPuss>();
            m_leaderboardButtonTutorialText = GameObject.Find("LeaderboardButtonTutorialText").GetComponent<Text>();
            m_muteUnmuteButtonTutorialText = GameObject.Find("MuteButtonTutorialText").GetComponent<Text>();
            m_nextButtonImage = GameObject.Find("NextButton").GetComponent<Image>();
            m_pauseButton = GameObject.Find("PF_PauseButton").GetComponent<Button>();
            m_pauseButtonImage = GameObject.Find("PF_PauseButton").GetComponent<Image>();
            m_pauseButtonTutorialText = GameObject.Find("PauseButtonTutorialText").GetComponent<Text>();
            m_pauseMenuObj = GameObject.Find("PauseMenu");
            _pauseMenuImage = m_pauseMenuObj.GetComponent<Image>();
            m_polaroidsCountText = GameObject.Find("PolaroidsCountText").GetComponent<Text>();
            m_polaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
            m_polaroidImage = GameObject.Find("PolaroidImage").GetComponent<Image>();
            _resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            m_selfieButtonImage = GameObject.Find("SelfieButton").GetComponent<Image>();
            m_selfiePanelImage = GameObject.Find("SelfiePanel").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            _swipeDownHandAnimator = GameObject.Find("SwipeDownHand").GetComponent<Animator>();
            _swipeUpHandAnimator = GameObject.Find("SwipeUpHand").GetComponent<Animator>();
            _swipeDownHandImage = GameObject.Find("SwipeDownHand").GetComponent<Image>();
            _swipeUpHandImage = GameObject.Find("SwipeUpHand").GetComponent<Image>();
            _swipeHandOKButtonImage = GameObject.Find("SwipeHandOKButton").GetComponent<Image>();
            _swipeHandPanelImage = GameObject.Find("SwipeHandPanel").GetComponent<Image>();
            m_uiButtonsTutorialMenuObj = GameObject.Find("UIButtonsTutorialMenu");
            m_uiButtonsTutorialMenuImage = m_uiButtonsTutorialMenuObj.GetComponent<Image>();

            if(m_firstTimeUIButtonsTutorial == 0)
            {
                m_arrow01Image.enabled = true;
                m_chimpionshipBeltButton.interactable = false;
                m_leaderboardButtonTutorialText.enabled = true;
                m_muteButton.interactable = false;
                m_nextButtonImage.enabled = true;
                m_pauseButton.interactable = false;
                m_polaroidsCountText.enabled = false;
                m_polaroidImage.enabled = false;
                m_uiButtonsTutorialMenuImage.enabled = true;
                m_unmuteButton.interactable = false;
                Time.timeScale = 0;
            }

            if(MusicManager.m_musicSource != null && _soundManager.m_soundsSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }
            }
        }

        else
        {
            _backToLandLoseMenuImage = GameObject.Find("BackToLandLoseMenu").GetComponent<Image>();
            _backToLandLoseText = GameObject.Find("BackToLandLoseText").GetComponent<Text>();
            _backToLandWinMenuImage = GameObject.Find("BackToLandWinMenu").GetComponent<Image>();
            _backToLandWinText = GameObject.Find("BackToLandWinText").GetComponent<Text>();
            _backToLandWithSuperMenuImage = GameObject.Find("BackToLandWithSuperMenu").GetComponent<Image>();
            _backToLandWithSuperText = GameObject.Find("BackToLandWithSuperText").GetComponent<Text>();
            m_chimpionshipBeltButton = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Button>();
            m_chimpionshipBeltButtonImage = GameObject.Find("PF_ChimpionshipBeltButton").GetComponent<Image>();
            _continueButtonImage = GameObject.Find("ContinueButton").GetComponent<Image>();
            _firstTimePlayTutorialMenuImage = GameObject.Find("FirstTimePlayTutorialMenu").GetComponent<Image>();
            _firstTimePlayTutorialOKButtonImage = GameObject.Find("FirstTimePlayTutorialOKButton").GetComponent<Image>();
            _firstTimePlayTutorialText = GameObject.Find("FirstTimePlayTutorialText").GetComponent<Text>();
            m_highScoreLabelText = GameObject.Find("HighScoreLabelText").GetComponent<Text>();
            m_highScoreValueText = GameObject.Find("HighScoreValueText").GetComponent<Text>();
            m_muteButtonImage = GameObject.Find("MuteButton").GetComponent<Image>();
            m_pauseButtonImage = GameObject.Find("PF_PauseButton").GetComponent<Image>();
            m_pauseMenuObj = GameObject.Find("PauseMenu");
            _pauseMenuImage = m_pauseMenuObj.GetComponent<Image>();
            m_polaroidsCountText = GameObject.Find("PolaroidsCountText").GetComponent<Text>();
            m_polaroidsCountText.text = ScoreManager.m_polaroidsCount.ToString();
            m_polaroidImage = GameObject.Find("PolaroidImage").GetComponent<Image>();
            _resumeButtonImage = GameObject.Find("ResumeButton").GetComponent<Image>();
            _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            m_unmuteButtonImage = GameObject.Find("UnmuteButton").GetComponent<Image>();

            if(m_firstTimeWaterLevelTutorial == 0)
            {
                m_chimpionshipBeltButtonImage.enabled = false;
                _firstTimePlayTutorialMenuImage.enabled = true;
                _firstTimePlayTutorialText.enabled = true;
                m_highScoreLabelText.enabled = false;
                m_highScoreValueText.enabled = false;
                _firstTimePlayTutorialOKButtonImage.enabled = true;
                m_pauseButtonImage.enabled = false;
                m_polaroidsCountText.enabled = false;
                m_polaroidImage.enabled = false;
                Time.timeScale = 0;
            }

            if(MusicManager.m_musicSource != null)
            {
                if(!MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    MusicManager.m_musicSource.Play();
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 0)
                {
                    m_muteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = true;
                }

                else if(MusicManager.m_musicSource.isPlaying && m_playerMutedSounds == 1)
                {
                    MusicManager.m_musicSource.Pause();
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }

                else
                {
                    _soundManager.m_soundsSource.enabled = false;
                    m_unmuteButtonImage.enabled = true;
                }
            }
        }

        if(b_isMemoryLeakTestingMode)
        {
            if(m_currentScene == 1)
            {
                _memoryLeakTestText.enabled = true;
                _waterLevelButtonImage.enabled = true;
            }

            if(m_currentScene == 2)
            {
                _landLevelButtonImage.enabled = true;
                _memoryLeakTestText.enabled = true;
            }
        }
    }

    public void GoToFallingLevelButton()
    {
        SceneManager.LoadScene("FallingDown");
    }

    public void GoToLandLevelButton()
    {
        SceneManager.LoadScene("LandRunner");
    }

    public void GoToWaterLevelButton()
    {
        SceneManager.LoadScene("WaterSwimmer");
    }

    public void IAPCartButton()
    {
        _iapCartMenuObj.SetActive(true);
    }

    public void IAPCancelButton()
    {
        _iapCartMenuObj.SetActive(false);
    }

    public void IAPNoButton()
    {
        BhanuPrefs.DeleteScore();
        ScoreManager.m_supersCount = 0;
        BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
        SceneManager.LoadScene(m_currentScene);
    }

    public void IAPYesButton()
    {
        _iapCartMenuObj.SetActive(true);
    }

    public void MuteUnmuteButton()
    {
        if(m_firstTimeUIButtonsTutorial == 1 && m_currentScene > 0)
        {
            if(MusicManager.m_musicSource != null)
            {
                if(m_muteButtonImage.enabled)
                {
                    m_muteButtonImage.enabled = false;
                    MusicManager.m_musicSource.Pause();
                    m_playerMutedSounds = 1;
                    BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                    m_unmuteButtonImage.enabled = true;
                    _soundManager.m_soundsSource.enabled = false;
                }

                else if(!m_muteButtonImage.enabled)
                {
                    m_muteButtonImage.enabled = true;
                    MusicManager.m_musicSource.Play();
                    m_playerMutedSounds = 0;
                    BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                    m_unmuteButtonImage.enabled = false;
                    _soundManager.m_soundsSource.enabled = true;
                }
            }
        }

        else if(m_currentScene == 0)
        {
            if(MusicManager.m_musicSource != null)
            {
                if(m_muteButtonImage.enabled)
                {
                    m_muteButtonImage.enabled = false;
                    MusicManager.m_musicSource.Pause();
                    m_playerMutedSounds = 1;
                    BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                    m_unmuteButtonImage.enabled = true;
                }

                else if(!m_muteButtonImage.enabled)
                {
                    m_muteButtonImage.enabled = true;
                    MusicManager.m_musicSource.Play();
                    m_playerMutedSounds = 0;
                    BhanuPrefs.SetSoundsStatus(m_playerMutedSounds);
                    m_unmuteButtonImage.enabled = false;
                }
            }
        }
    }

    public void NextButton()
    {
        if(m_arrow01Image.enabled)
        {
            m_arrow01Image.enabled = false;
            m_arrow02Image.enabled = true;
            m_leaderboardButtonTutorialText.enabled = false;
            m_pauseButton.interactable = true;
            m_pauseButtonTutorialText.enabled = true;
        }

        else if(m_arrow02Image.enabled)
        {
            m_arrow02Image.enabled = false;
            m_arrow03Image.enabled = true;

            if(m_playerMutedSounds == 0)
            {
                m_muteButton.interactable = true;
                m_muteButtonImage.enabled = true;
                m_unmuteButton.interactable = true;
            }
            else
            {
                m_muteButton.interactable = true;
                m_unmuteButton.interactable = true;
                m_unmuteButtonImage.enabled = true;
            }
            
            m_muteUnmuteButtonTutorialText.enabled = true;
            m_pauseButtonTutorialText.enabled = false;
        }

        else if(m_arrow03Image.enabled)
        {
            m_arrow03Image.enabled = false;
            m_arrow04Image.enabled = true;
            m_chimpionshipBeltButton.interactable = true;
            m_chimpionshipBeltButtonTutorialText.enabled = true;
            m_muteUnmuteButtonTutorialText.enabled = false;
        }

        else
        {
            m_arrow01Image.enabled = false;
            m_arrow02Image.enabled = false;
            m_arrow03Image.enabled = false;
            m_arrow04Image.enabled = false;
            m_chimpionshipBeltButtonTutorialText.enabled = false;
            m_firstTimeUIButtonsTutorial = 1;
            m_leaderboardButtonTutorialText.enabled = false;
            m_muteUnmuteButtonTutorialText.enabled = false;
            m_nextButtonImage.enabled = false;
            m_pauseButtonTutorialText.enabled = false;
            m_uiButtonsTutorialMenuImage.enabled = false;
            m_polaroidsCountText.enabled = true;
            m_polaroidImage.enabled = true;
            Time.timeScale = 1;

            if(!b_isFirstTimeTutorialTestingMode)
            {
                BhanuPrefs.SetFirstTimeUIButtonsTutorialStatus(m_firstTimeUIButtonsTutorial);
            }
        }
    }

    public void PauseButton()
	{
        if(m_firstTimeUIButtonsTutorial == 1)
        {
            if(MusicManager.m_musicSource != null)
            {
                MusicManager.m_musicSource.Pause();
            }

            m_muteButtonImage.enabled = false;
            m_unmuteButtonImage.enabled = false;

            if(m_chimpionshipBeltButtonImage != null)
            {
                m_chimpionshipBeltButtonImage.enabled = false;
            }
        
            if(_exitButtonImage != null)
            {
                _exitButtonImage.enabled = true;
            }

		    m_highScoreLabelText.enabled = false;
		    m_highScoreValueText.enabled = false;
            m_polaroidsCountText.enabled = false;
            m_polaroidImage.enabled = false;
        
            if(m_currentScene < 2)
            {
                _iapCartButtonImage.enabled = true;
            }
            
		    m_pauseButtonImage.enabled = false;
		    _pauseMenuImage.enabled = true;
		    _resumeButtonImage.enabled = true;

		    if(m_selfiePanelImage != null)
		    {
			    m_selfieButtonImage.enabled = false;	
		    }

		    Time.timeScale = 0;
        }
	}

	public void PlayButton()
	{
		SceneManager.LoadScene("LandRunner");
	}

	public void QuitButton()
	{
        MusicManager.m_musicSource.Pause();
        m_muteButtonImage.enabled = false;
        b_quitButtonTapped = true;
        _playButtonImage.enabled = false;
		_quitButtonImage.enabled = false;
		_quitMenuImage.enabled = true;
		_quitAcceptButtonImage.enabled = true;
		_quitCancelButtonImage.enabled = true;
		_quitText.enabled = true;
        m_unmuteButtonImage.enabled = false;
	}

	public void QuitAcceptButton()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

	public void QuitCancelButton()
	{
        if(m_playerMutedSounds == 0)
        {
            MusicManager.m_musicSource.Play();
            m_muteButtonImage.enabled = true;
        }
        else
        {
            m_unmuteButtonImage.enabled = true;
        }

		_playButtonImage.enabled = true;
		_quitButtonImage.enabled = true;
        b_quitButtonTapped = false;
		_quitMenuImage.enabled = false;
		_quitAcceptButtonImage.enabled = false;
		_quitCancelButtonImage.enabled = false;
		_quitText.enabled = false;
	}

	public void ResumeButton()
	{
        if(MusicManager.m_musicSource != null)
        {
            if(m_playerMutedSounds == 0 && !MusicManager.m_musicSource.isPlaying)
            {
                MusicManager.m_musicSource.Play();
                m_muteButtonImage.enabled = true;
            }

            else if(m_playerMutedSounds == 1)
            {
                m_unmuteButtonImage.enabled = true;
            }
        }

        if(m_chimpionshipBeltButtonImage != null)
        {
            m_chimpionshipBeltButtonImage.enabled = true;
        }

        if(_exitButtonImage != null)
        {
            _exitButtonImage.enabled = false;
        }

		m_highScoreLabelText.enabled = true;
		m_highScoreValueText.enabled = true;
        m_polaroidsCountText.enabled = true;
        m_polaroidImage.enabled = true;
        
        if(m_currentScene < 2)
        {
            _iapCartButtonImage.enabled = false;
        }

		m_pauseButtonImage.enabled = true;
		_pauseMenuImage.enabled = false;
        _resumeButtonImage.enabled = false;

		Time.timeScale = 1;
	}

	public void SelfieButton()
	{
		_soundManager.m_soundsSource.clip = _soundManager.m_selfie;
		
        if(_soundManager.m_soundsSource.enabled)
        {
            _soundManager.m_soundsSource.Play();
        }

		m_selfieButtonImage.enabled = false;

		if(_bSelfieFlashEnabled)
		{
			m_selfiePanelImage.enabled = true;
			Invoke("EndFlash" , 0.25f);
		}

		if(_landChimp.m_isSlipping)
        {
            ScoreManager.m_scoreValue += 60;
        }

        else if(_landChimp.m_isSuper) 
		{
			ScoreManager.m_scoreValue += 200;
		} 

		else 
		{
			ScoreManager.m_scoreValue += 20;
		}

		BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
        m_highScoreValueText.text = ScoreManager.m_scoreValue.ToString();
	}

    public void SwipeHandOKButton()
    {
        m_chimpionshipBeltButtonImage.enabled = true;
        m_highScoreLabelText.enabled = true;
        m_highScoreValueText.enabled = true;

        if(m_playerMutedSounds == 0)
        {
            m_muteButtonImage.enabled = true;
        }
        else
        {
            m_unmuteButtonImage.enabled = true;
        }
        
        m_pauseButtonImage.enabled = true;
        _swipeDownHandAnimator.enabled = false;
        _swipeUpHandAnimator.enabled = false;
        _swipeDownHandImage.enabled = false;
        _swipeUpHandImage.enabled = false;
        _swipeHandOKButtonImage.enabled = false;
        _swipeHandPanelImage.enabled = false;
        Time.timeScale = 1;
    }
}
