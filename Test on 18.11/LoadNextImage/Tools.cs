﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
namespace LoadNextImage
{
    static class Tools
    {
        public static void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
        out HTuple hv_ImageFiles)
        {
            // Local iconic variables 

            // Local control variables 

            HTuple hv_ImageDirectoryIndex = new HTuple();
            HTuple hv_ImageFilesTmp = new HTuple(), hv_CurrentImageDirectory = new HTuple();
            HTuple hv_HalconImages = new HTuple(), hv_OS = new HTuple();
            HTuple hv_Directories = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Length = new HTuple(), hv_NetworkDrive = new HTuple();
            HTuple hv_Substring = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_AllFiles = new HTuple(), hv_i = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = new HTuple(hv_Extensions);

            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            //This procedure returns all files in a given directory
            //with one of the suffixes specified in Extensions.
            //
            //Input parameters:
            //ImageDirectory: Directory or a tuple of directories with images.
            //   If a directory is not found locally, the respective directory
            //   is searched under %HALCONIMAGES%/ImageDirectory.
            //   See the Installation Guide for further information
            //   in case %HALCONIMAGES% is not set.
            //Extensions: A string tuple containing the extensions to be found
            //   e.g. ['png','tif',jpg'] or others
            //If Extensions is set to 'default' or the empty string '',
            //   all image suffixes supported by HALCON are used.
            //Options: as in the operator list_files, except that the 'files'
            //   option is always used. Note that the 'directories' option
            //   has no effect but increases runtime, because only files are
            //   returned.
            //
            //Output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                "default")))) != 0)
            {
                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            hv_ImageFiles.Dispose();
            hv_ImageFiles = new HTuple();
            //Loop through all given image directories.
            for (hv_ImageDirectoryIndex = 0; (int)hv_ImageDirectoryIndex <= (int)((new HTuple(hv_ImageDirectory.TupleLength()
                )) - 1); hv_ImageDirectoryIndex = (int)hv_ImageDirectoryIndex + 1)
            {
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                hv_CurrentImageDirectory.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_CurrentImageDirectory = hv_ImageDirectory.TupleSelect(
                        hv_ImageDirectoryIndex);
                }
                if ((int)(new HTuple(hv_CurrentImageDirectory.TupleEqual(""))) != 0)
                {
                    hv_CurrentImageDirectory.Dispose();
                    hv_CurrentImageDirectory = ".";
                }
                hv_HalconImages.Dispose();
                HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ";");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ":");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                hv_Directories.Dispose();
                hv_Directories = new HTuple(hv_CurrentImageDirectory);
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Directories = hv_Directories.TupleConcat(
                                ((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_CurrentImageDirectory);
                            hv_Directories.Dispose();
                            hv_Directories = ExpTmpLocalVar_Directories;
                        }
                    }
                }
                hv_Length.Dispose();
                HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NetworkDrive.Dispose();
                    HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_NetworkDrive);
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                            )).TupleGreater(1))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Substring.Dispose();
                                HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1,
                                    out hv_Substring);
                            }
                            if ((int)((new HTuple(hv_Substring.TupleEqual("//"))).TupleOr(new HTuple(hv_Substring.TupleEqual(
                                "\\\\")))) != 0)
                            {
                                if (hv_NetworkDrive == null)
                                    hv_NetworkDrive = new HTuple();
                                hv_NetworkDrive[hv_Index] = 1;
                            }
                        }
                    }
                }
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_FileExists.Dispose();
                        HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                    }
                    if ((int)(hv_FileExists) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_AllFiles.Dispose();
                            HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                                hv_Options), out hv_AllFiles);
                        }
                        hv_ImageFilesTmp.Dispose();
                        hv_ImageFilesTmp = new HTuple();
                        for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                            )) - 1); hv_i = (int)hv_i + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Selection.Dispose();
                                HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                    hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = hv_ImageFilesTmp.TupleConcat(
                                        hv_Selection);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("\\\\")).TupleConcat(
                                "replace_all"), "/", out ExpTmpOutVar_0);
                            hv_ImageFilesTmp.Dispose();
                            hv_ImageFilesTmp = ExpTmpOutVar_0;
                        }
                        if ((int)(hv_NetworkDrive.TupleSelect(hv_Index)) != 0)
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = "/" + hv_ImageFilesTmp;
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        else
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                        }
                        break;
                    }
                }
                //Concatenate the output image paths.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ImageFiles = hv_ImageFiles.TupleConcat(
                            hv_ImageFilesTmp);
                        hv_ImageFiles.Dispose();
                        hv_ImageFiles = ExpTmpLocalVar_ImageFiles;
                    }
                }
            }

            hv_Extensions_COPY_INP_TMP.Dispose();
            hv_ImageDirectoryIndex.Dispose();
            hv_ImageFilesTmp.Dispose();
            hv_CurrentImageDirectory.Dispose();
            hv_HalconImages.Dispose();
            hv_OS.Dispose();
            hv_Directories.Dispose();
            hv_Index.Dispose();
            hv_Length.Dispose();
            hv_NetworkDrive.Dispose();
            hv_Substring.Dispose();
            hv_FileExists.Dispose();
            hv_AllFiles.Dispose();
            hv_i.Dispose();
            hv_Selection.Dispose();

            return;
        }

        internal static void list_image_files(HTuple hv_ImageDirectory, string v, object p, out object hv_ImageFiles)
        {
            throw new NotImplementedException();
        }
    }
}
