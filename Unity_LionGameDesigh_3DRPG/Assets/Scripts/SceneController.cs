using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ryan 
{
    /// <summary>
    /// 場景控制
    /// 指定要前往哪個場景
    /// 離開整個遊戲
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// 載入指定場景
        /// </summary>
        /// <param name="sceneName">場景名稱</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        /// <summary>
        /// 離開遊戲
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}

