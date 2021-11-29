using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ryan 
{
    /// <summary>
    /// ��������
    /// ���w�n�e�����ӳ���
    /// ���}��ӹC��
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// ���J���w����
        /// </summary>
        /// <param name="sceneName">�����W��</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        /// <summary>
        /// ���}�C��
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}

