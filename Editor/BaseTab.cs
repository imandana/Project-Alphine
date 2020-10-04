﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SFB;

/*BASE TAB v 1.0
 * This base tab used for base tabbing, so we won't use
 * repeated codes that used in many occasion.
 * This code still under development, make sure to contact
 * jodysag5@gmail.com for further question about tabbing.
 * 
 */

public abstract class BaseTab
{
    #region Features
    /// <summary>
    /// This called when actor list on active.
    /// pick item to load it.
    /// </summary>
    /// <param name="index">index of actor in a list.</param>
    public abstract void ItemTabLoader(int index);

    ///<summary>
    /// Folder checker, create folder if it doesnt exist already, Might refactor into one liner if
    ///</summary>
    public static void FolderChecker()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Data");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/ActorData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "ActorData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Image"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Image");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/ClassesData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "ClassesData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/SkillData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "SkillData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/ItemData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "ItemData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/WeaponData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "WeaponData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/ArmorData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "ArmorData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Data/EnemyData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources/Data", "EnemyData");
        }
    }

    /// <summary>
    /// Create Texture for GUI skin.
    /// </summary>
    /// <param name="width">pixel width of GUI Skin.</param>
    /// <param name="height">pixel height of GUI Skin.</param>
    /// <param name="col">Color of GUI Skin.</param>
    /// <returns></returns>
    public Texture2D CreateTexture(int width, int height, Color col)
    {
        //Create array of color.
        Color[] colPixel = new Color[width * height];

        for (int i = 0; i < colPixel.Length; ++i)
        {
            colPixel[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(colPixel);
        result.Apply();
        return result;
    }


    /// <summary>
    /// Create a texture from a sprite (Used for changing actors' images)
    /// </summary>
    /// <param name="sprite">the sprite that wants to be converted into texture</param>
    /// <returns></returns>
    public Texture2D TextureToSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    int counter = 0;

    /// <summary>
    /// Change Maximum function , when we change the size
    /// and click Change Maximum button in Editor, it will update
    /// and change the size while creating new data.
    /// </summary>
    /// <param name="size">get size from actorSize</param>
    /// <param name="listTabData">list of item that you want to display.</param>
    /// <param name="dataTabName">get size from actorSize</param>
    public void ChangeMaximum(int dataSize, List<BaseData> listTabData, List<string> dataTabName, string dataPath)
    {

        //This count only useful when we doesn't have a name yet.
        //you can remove this when decide a new format later.
        while (counter <= dataSize)
        {
            listTabData.Add(ScriptableObject.CreateInstance<BaseData>());

            AssetDatabase.CreateAsset(listTabData[counter], dataPath + counter + ".asset");
            AssetDatabase.SaveAssets();
            dataTabName.Add(listTabData[counter].dataName);
            counter++;
        }
        if (counter > dataSize)
        {
            listTabData.RemoveRange(dataSize, listTabData.Count - dataSize);
            dataTabName.RemoveRange(dataSize, dataTabName.Count - dataSize);
            for (int i = dataSize; i <= counter; i++)
            {
                AssetDatabase.DeleteAsset(dataPath + i + ".asset");
            }
            AssetDatabase.SaveAssets();
            counter = dataSize;
        }
    }

    ExtensionFilter[] fileExtensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
                new ExtensionFilter("Sound Files", "mp3", "wav" ),
                new ExtensionFilter("All Files", "*" ),
        };

    public Sprite ImageChanger(int index, string panelName, string assetPath)
    {
        string relativepath;
        string[] path = StandaloneFileBrowser.OpenFilePanel(panelName, assetPath, fileExtensions, false);
        
        if (path.Length != 0)
        {
            relativepath = "Image/";
            relativepath += Path.GetFileNameWithoutExtension(path[0]);
            Sprite imageChosen = Resources.Load<Sprite>(relativepath);
            return imageChosen;
        }

        Debug.LogError("Image Changer should have path directly to this!");
        return null;
    }
    #endregion
}
