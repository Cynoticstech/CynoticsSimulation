using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy_redirect_script : MonoBehaviour
{
    public string externalLink;
    public IEnumerator TermsCond, CokkiePol, PrivPol, RefundPol;
    public void OpenExternalLink()
    {
        // Check if the external link is not empty
        if (!string.IsNullOrEmpty(externalLink))
        {
            // Open the external link
            Application.OpenURL(externalLink);
        }
    }


}
