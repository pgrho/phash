/*

pHash, the open source perceptual hash library
Copyright (C) 2009 Aetilius, Inc.
All rights reserved.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

Evan Klinger - eklinger@phash.org
D Grant Starkweather - dstarkweather@phash.org

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    public class pHash
    {
        // pHash.h

        //#ifndef _PHASH_H
        //#define _PHASH_H

        //#ifndef _WIN32
        //#include <pHash-config.h>
        //#include <unistd.h>
        //#include <sys/mman.h>
        //#include <stdint.h>
        //#endif

        //#include <limits.h>
        //#include <math.h>
        //#include "dirent.h"
        //#include <errno.h>
        //#include <sys/types.h>
        //#include <sys/stat.h>
        //#include <fcntl.h>
        //#include <string.h>
        //#include <stdio.h>
        //#include <stdlib.h>

        //#define __STDC_CONSTANT_MACROS

        //#if defined(HAVE_IMAGE_HASH) || defined(HAVE_VIDEO_HASH)
        //#define cimg_debug 0
        //#define cimg_display 0
        //#include "CImg.h"
        //using namespace cimg_library;
        //#endif

        //#ifdef HAVE_PTHREAD
        //#include <pthread.h>
        //#endif

        //#if !defined(__GLIBC__) && !defined(_WIN32)
        //#include <sys/param.h>
        //#include <sys/sysctl.h>
        //#endif

        //using namespace std;

        //#define SQRT_TWO 1.4142135623730950488016887242097

        //#ifndef ULLONG_MAX
        //#define ULLONG_MAX 18446744073709551615ULL
        //#endif

        //#define ROUNDING_FACTOR(x) (((x) >= 0) ? 0.5 : -0.5)

        //#ifndef _WIN32
        //typedef unsigned _uint64 ulong;
        //typedef signed _int64 long64;
        //#else
        //typedef unsigned long long ulong;
        //typedef signed long long long64;
        //typedef unsigned char byte;
        //typedef unsigned int uint32_t;
        //#endif

        //#ifdef __cplusplus
        //extern "C" {
        //#endif

        //const int MaxFileSize = (1 << 30); /* 1GB file size limit (for mvp files) */
        //const off_t HeaderSize = 64;     /* header size for mvp file */

        //typedef struct ph_file_offset
        //{
        //    off_t offset;
        //    byte fileno;
        //}
        //FileIndex;

        ///* structure for a single hash */
        //typedef struct ph_datapoint
        //{
        //    char* id;
        //    void* hash;
        //    float* path;
        //    uint32_t hash_length;
        //    byte hash_type;
        //}
        //DP;

        //typedef struct ph_slice
        //{
        //    DP** hash_p;
        //    int n;
        //    void* hash_params;
        //}
        //slice;

        //struct BinHash
        //{
        //    byte* hash;
        //    uint32_t bytelength;
        //    uint32_t byteidx; // used by addbit()
        //    byte bitmask;  // used by addbit()

        //    /*
        //	 * add a single bit to hash. the bits are
        //	 * written from left to right.
        //	 */
        //    int addbit(byte bit)
        //    {
        //        if (bitmask == 0)
        //        {
        //            bitmask = 128; // reset bitmask to "10000000"
        //            byteidx++;     // jump to next byte in array
        //        }

        //        if (byteidx >= bytelength) return -1;

        //        if (bit == 1) *(hash + byteidx) |= bitmask;
        //        bitmask >>= 1;
        //        return 0;
        //    }
        //};

        //BinHash* _ph_bmb_new(uint32_t bytelength);
        //void ph_bmb_free(BinHash* binHash);

        // /*! /brief Radon Projection info
        // */
        //#ifdef HAVE_IMAGE_HASH
        //typedef struct ph_projections
        //{
        //    CImg<byte>* R;           //contains projections of image of angled lines through center
        //    int* nb_pix_perline;        //the head of int array denoting the number of pixels of each line
        //    int size;                   //the size of nb_pix_perline
        //}
        //Projections;
        //#endif

        // /*! /brief feature vector info
        // */
        //typedef struct ph_feature_vector
        //{
        //    double* features;           //the head of the feature array of double's
        //    int size;                   //the size of the feature array
        //}
        //Features;

        // /*! /brief Digest info
        // */
        //typedef struct ph_digest
        //{
        //    char* id;                   //hash id
        //    byte* coeffs;            //the head of the digest integer coefficient array
        //    int size;                   //the size of the coeff array
        //}
        //Digest;

        ///* variables for textual hash */
        //const int KgramLength = 50;
        //const int WindowLength = 100;
        //const int delta = 1;

        //#define ROTATELEFT(x, bits)  (((x)<<(bits)) | ((x)>>(64-bits)))

        //typedef struct ph_hash_point
        //{
        //    ulong hash;
        //    off_t index; /*pos of hash in orig file */
        //}
        //TxtHashPoint;

        //typedef struct ph_match
        //{
        //    off_t first_index; /* offset into first file */
        //    off_t second_index; /* offset into second file */
        //    uint32_t length;    /*length of match between 2 files */
        //}
        //TxtMatch;

        //#ifdef HAVE_PTHREAD
        //int ph_num_threads();
        //#endif

        ///* /brief alloc a single data point
        // *  allocates path array, does nto set id or path
        // */
        //DP* ph_malloc_datapoint(int hashtype);

        ///** /brief free a datapoint and its path
        // *
        // */
        //void ph_free_datapoint(DP* dp);

        // /*! /brief copyright information
        // */
        //const char* ph_about();

        // /*! /brief radon function
        // *  Find radon projections of N lines running through the image center for lines angled 0
        // *  to 180 degrees from horizontal.
        // *  /param img - CImg src image
        // *  /param  N  - int number of angled lines to consider.
        // *  /param  projs - (out) Projections struct
        // *  /return int value - less than 0 for error
        // */
        //#ifdef HAVE_IMAGE_HASH
        //int ph_radon_projections(const CImg<byte> &img, int N, Projections &projs);

        // /*! /brief feature vector
        // *         compute the feature vector from a radon projection map.
        // *  /param  projs - Projections struct
        // *  /param  fv    - (out) Features struct
        // *  /return int value - less than 0 for error
        //*/
        //int ph_feature_vector(const Projections &projs, Features &fv);

        // /*! /brief dct
        // *  Compute the dct of a given vector
        // *  /param R - vector of input series
        // *  /param D - (out) the dct of R
        // *  /return  int value - less than 0 for error
        //*/
        //int ph_dct(const Features &fv, Digest &digest);

        // /*! /brief cross correlation for 2 series
        // *  Compute the cross correlation of two series vectors
        // *  /param x - Digest struct
        // *  /param y - Digest struct
        // *  /param pcc - double value the peak of cross correlation
        // *  /param threshold - double value for the threshold value for which 2 images
        // *                     are considered the same or different.
        // *  /return - int value - 1 (true) for same, 0 (false) for different, < 0 for error
        // */

        //int ph_crosscorr(const Digest &x,const Digest &y, double &pcc, double threshold = 0.90);

        // /*! /brief image digest
        // *  Compute the image digest for an image given the input image
        // *  /param img - CImg object representing an input image
        // *  /param sigma - double value for the deviation for a gaussian filter function
        // *  /param gamma - double value for gamma correction on the input image
        // *  /param digest - (out) Digest struct
        // *  /param N      - int value for the number of angles to consider.
        // *  /return       - less than 0 for error
        // */
        //int _ph_image_digest(const CImg<byte> &img, double sigma, double gamma, Digest &digest, int N = 180);

        // /*! /brief image digest
        // *  Compute the image digest given the file name.
        // *  /param file - string value for file name of input image.
        // *  /param sigma - double value for the deviation for gaussian filter
        // *  /param gamma - double value for gamma correction on the input image.
        // *  /param digest - Digest struct
        // *  /param N      - int value for number of angles to consider
        // */
        //int ph_image_digest(const char* file, double sigma, double gamma, Digest &digest, int N = 180);

        // /*! /brief compare 2 images
        // *  /param imA - CImg object of first image
        // *  /param imB - CImg object of second image
        // *  /param pcc   - (out) double value for peak of cross correlation
        // *  /param sigma - double value for the deviation of gaussian filter
        // *  /param gamma - double value for gamma correction of images
        // *  /param N     - int number for the number of angles of radon projections
        // *  /param theshold - double value for the threshold
        // *  /return int 0 (false) for different images, 1 (true) for same image, less than 0 for error
        // */
        //int _ph_compare_images(const CImg<byte> &imA,const CImg<byte> &imB, double &pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90);

        // /*! /brief compare 2 images
        // *  Compare 2 images given the file names
        // *  /param file1 - char string of first image file
        // *  /param file2 - char string of second image file
        // *  /param pcc   - (out) double value for peak of cross correlation
        // *  /param sigma - double value for deviation of gaussian filter
        // *  /param gamma - double value for gamma correction of images
        // *  /param N     - int number for number of angles
        // *  /return int 0 (false) for different image, 1 (true) for same images, less than 0 for error
        // */
        //int ph_compare_images(const char* file1, const char* file2, double &pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90);


        // /*! /brief compute dct robust image hash
        // *  /param file string variable for name of file
        // *  /param hash of type ulong (must be 64-bit variable)
        // *  /return int value - -1 for failure, 1 for success
        // */
        //int ph_dct_imagehash(const char* file, ulong &hash);


        //int ph_bmb_imagehash(const char* file, byte method, BinHash** ret_hash);
        //#endif

        //#ifdef HAVE_PTHREAD
        //DP** ph_dct_image_hashes(char* files[], int count, int threads = 0);
        //#endif

        //#ifdef HAVE_VIDEO_HASH
        //static CImgList<byte>* ph_getKeyFramesFromVideo(const char* filename);

        //ulong* ph_dct_videohash(const char* filename, int &Length);

        //DP** ph_dct_video_hashes(char* files[], int count, int threads = 0);

        //double ph_dct_videohash_dist(ulong* hashA, int N1, ulong* hashB, int N2, int threshold = 21);
        //#endif

        ///* ! /brief dct video robust hash
        // *   Compute video hash based on the dct of normalized video 32x32x64 cube
        // *   /param file name of file
        // *   /param hash ulong value for hash value
        // *   /return int value - less than 0 for error
        // */
        //#ifdef HAVE_IMAGE_HASH
        //int ph_hamming_distance(const ulong hash1,const ulong hash2);

        ///** /brief create a list of datapoint's directly from a directory of image files
        // *  /param dirname - path and name of directory containg all image file names
        // *  /param capacity - int value for upper limit on number of hashes
        // *  /param count - number of hashes created (out param)
        // *  /return pointer to a list of DP pointers (null for error)
        // */

        //DP** ph_read_imagehashes(const char* dirname, int capacity, int &count);

        ///** /brief create MH image hash for filename image
        //*   /param filename - string name of image file
        //*   /param N - (out) int value for length of image hash returned
        //*   /param alpha - int scale factor for marr wavelet (default=2)
        //*   /param lvl   - int level of scale factor (default = 1)
        //*   /return byte array
        //**/
        //byte* ph_mh_imagehash(const char* filename, int &N, float alpha = 2.0f, float lvl = 1.0f);
        //#endif

        ///** /brief get all the filenames in specified directory
        // *  /param dirname - string value for path and filename
        // *  /param cap - int value for upper limit to number of files
        // *  /param count - int value for number of file names returned
        // *  /return array of pointers to string file names (null for error)
        // **/

        //char** ph_readfilenames(const char* dirname, int &count);

        ///** /brief textual hash for file
        // *  /param filename - char* name of file
        // *  /param nbpoints - int length of array of return value (out)
        // *  /return TxtHashPoint* array of hash points with respective index into file.
        // **/
        //TxtHashPoint* ph_texthash(const char* filename, int* nbpoints);

        ///** /brief compare 2 text hashes
        // *  /param hash1 -TxtHashPoint
        // *  /param N1 - int length of hash1
        // *  /param hash2 - TxtHashPoint
        // *  /param N2 - int length of hash2
        // *  /param nbmatches - int number of matches found (out)
        // *  /return TxtMatch* - list of all matches
        // **/
        //TxtMatch* ph_compare_text_hashes(TxtHashPoint* hash1, int N1, TxtHashPoint* hash2, int N2, int* nbmatches);

        /// <summary>
        /// random char mapping for textual hash
        /// </summary>
        private static readonly ulong[] textkeys = {
            15498727785010036736UL,
            7275080914684608512UL,
            14445630958268841984UL,
            14728618948878663680UL,
            16816925489502355456UL,
            3644179549068984320UL,
            6183768379476672512UL,
            14171334718745739264UL,
            5124038997949022208UL,
            10218941994323935232UL,
            8806421233143906304UL,
            11600620999078313984UL,
            6729085808520724480UL,
            9470575193177980928UL,
            17565538031497117696UL,
            16900815933189128192UL,
            11726811544871239680UL,
            13231792875940872192UL,
            2612106097615437824UL,
            11196599515807219712UL,
            300692472869158912UL,
            4480470094610169856UL,
            2531475774624497664UL,
            14834442768343891968UL,
            2890219059826130944UL,
            7396118625003765760UL,
            2394211153875042304UL,
            2007168123001634816UL,
            18426904923984625664UL,
            4026129272715345920UL,
            9461932602286931968UL,
            15478888635285110784UL,
            11301210195989889024UL,
            5460819486846222336UL,
            11760763510454222848UL,
            9671391611782692864UL,
            9104999035915206656UL,
            17944531898520829952UL,
            5395982256818880512UL,
            14229038033864228864UL,
            9716729819135213568UL,
            14202403489962786816UL,
            7382914959232991232UL,
            16445815627655938048UL,
            5226234609431216128UL,
            6501708925610491904UL,
            14899887495725449216UL,
            16953046154302455808UL,
            1286757727841812480UL,
            17511993593340887040UL,
            9702901604990058496UL,
            1587450200710971392UL,
            3545719622831439872UL,
            12234377379614556160UL,
            16421892977644797952UL,
            6435938682657570816UL,
            1183751930908770304UL,
            369360057810288640UL,
            8443106805659205632UL,
            1163912781183844352UL,
            4395489330525634560UL,
            17905039407946137600UL,
            16642801425058889728UL,
            15696699526515523584UL,
            4919114829672742912UL,
            9956820861803560960UL,
            6921347064588664832UL,
            14024113865587949568UL,
            9454608686614839296UL,
            12317329321407545344UL,
            9806407834332561408UL,
            724594440630435840UL,
            8072988737660780544UL,
            17189322793565552640UL,
            17170410068286373888UL,
            13299223355681931264UL,
            5244287645466492928UL,
            13623553490302271488UL,
            11805525436274835456UL,
            6531045381898240000UL,
            12688803018523541504UL,
            3061682967555342336UL,
            8118495582609211392UL,
            16234522641354981376UL,
            15296060347169898496UL,
            6093644486544457728UL,
            4223717250303000576UL,
            16479812286668603392UL,
            6463004544354746368UL,
            12666824055962206208UL,
            17643725067852447744UL,
            10858493883470315520UL,
            12125119390198792192UL,
            15839782419201785856UL,
            8108449336276287488UL,
            17044234219871535104UL,
            7349859215885729792UL,
            15029796409454886912UL,
            12621604020339867648UL,
            16804467902500569088UL,
            8900381657152880640UL,
            3981267780962877440UL,
            17529062343131004928UL,
            16973370403403595776UL,
            2723846500818878464UL,
            16252728346297761792UL,
            11825849685375975424UL,
            7968134154875305984UL,
            11429537762890481664UL,
            5184631047941259264UL,
            14499179536773545984UL,
            5671596707704471552UL,
            8246314024086536192UL,
            4170931045673205760UL,
            3459375275349901312UL,
            5095630297546883072UL,
            10264575540807598080UL,
            7683092525652901888UL,
            3128698510505934848UL,
            16727580085162344448UL,
            1903172507905556480UL,
            2325679513238765568UL,
            9139329894923108352UL,
            14028291906694283264UL,
            18165461932440551424UL,
            17247779239789330432UL,
            12625782052856266752UL,
            7068577074616729600UL,
            13830831575534665728UL,
            6800641999486582784UL,
            5426300911997681664UL,
            4284469158977994752UL,
            10781909780449460224UL,
            4508619181419134976UL,
            2811095488672038912UL,
            13505756289858273280UL,
            2314603454007345152UL,
            14636945174048014336UL,
            3027146371024027648UL,
            13744141225487761408UL,
            1374832156869656576UL,
            17526325907797573632UL,
            968993859482681344UL,
            9621146180956192768UL,
            3250512879761227776UL,
            4428369143422517248UL,
            14716776478503075840UL,
            13515088420568825856UL,
            12111461669075419136UL,
            17845474997598945280UL,
            11795924440611553280UL,
            14014634185570910208UL,
            1724410437128159232UL,
            2488510261825110016UL,
            9596182018555641856UL,
            1443128295859159040UL,
            1289545427904888832UL,
            3775219997702356992UL,
            8511705379065823232UL,
            15120377003439554560UL,
            10575862005778874368UL,
            13938006291063504896UL,
            958102097297932288UL,
            2911027712518782976UL,
            18446625472482639872UL,
            3769197585969971200UL,
            16416784002377056256UL,
            2314484861370368000UL,
            18406142768607920128UL,
            997186299691532288UL,
            16058626086858129408UL,
            1334230851768025088UL,
            76768133779554304UL,
            17027619946340810752UL,
            10955377032724217856UL,
            3327281022130716672UL,
            3009245016053776384UL,
            7225409437517742080UL,
            16842369442699542528UL,
            15120706693719130112UL,
            6624140361407135744UL,
            10191549809601544192UL,
            10688596805580488704UL,
            8348550798535294976UL,
            12680060080016588800UL,
            1838034750426578944UL,
            9791679102984388608UL,
            13969605507921477632UL,
            5613254748128935936UL,
            18303384482050211840UL,
            10643238446241415168UL,
            16189116753907810304UL,
            13794646699404165120UL,
            11601340543539347456UL,
            653400401306976256UL,
            13794528098177253376UL,
            15370538129509318656UL,
            17070184403684032512UL,
            16109012959547621376UL,
            15329936824407687168UL,
            18067370711965499392UL,
            13720894972696199168UL,
            16664167676175712256UL,
            18144138845745053696UL,
            12301770853917392896UL,
            9172800635190378496UL,
            3024675794166218752UL,
            15311015869971169280UL,
            16398210081298055168UL,
            1420301171746144256UL,
            11984978489980747776UL,
            4575606368995639296UL,
            11611850981347688448UL,
            4226831221851684864UL,
            12924157176120868864UL,
            5845166987654725632UL,
            6064865972278263808UL,
            4269092205395705856UL,
            1368028430456586240UL,
            11678120728997134336UL,
            4125732613736366080UL,
            12011266876698001408UL,
            9420493409195393024UL,
            17920379313140531200UL,
            5165863346527797248UL,
            10073893810502369280UL,
            13268163337608232960UL,
            2089657402327564288UL,
            8697334149066784768UL,
            10930432232036237312UL,
            17419594235325186048UL,
            8317960787322732544UL,
            6204583131022884864UL,
            15637017837791346688UL,
            8015355559358234624UL,
            59609911230726144UL,
            6363074407862108160UL,
            11040031362114387968UL,
            15370625789791830016UL,
            4314540415450611712UL,
            12460332533860532224UL,
            8908860206063026176UL,
            8890146784446251008UL,
            5625439441498669056UL,
            13135691436504645632UL,
            3367559886857568256UL,
            11470606437743329280UL,
            753813335073357824UL,
            7636652092253274112UL,
            12838634868199915520UL,
            12431934064070492160UL,
            11762384705989640192UL,
            6403157671188365312UL,
            3405683408146268160UL,
            11236019945420619776UL,
            11569021017716162560UL
        };

        //#ifdef __cplusplus
        //}
        //#endif

        //#endif

        // pHash.cpp

        //# include "pHash.h"
        //# ifndef _WIN32
        //# include "config.h"
        //#else
        //#define snprintf _snprintf
        //#endif
        //# ifdef HAVE_VIDEO_HASH
        //# include "cimgffmpeg.h"
        //#endif

        //# ifdef HAVE_PTHREAD
        //# include <pthread.h>

        int ph_num_threads()
            => Environment.ProcessorCount;

        //#endif



        const string phash_project = "{0}. Copyright 2008-2010 Aetilius, Inc.";
        const string phash_version = "0";

        public string ph_about()
        {
            if (phash_version != "0")
            {
#pragma warning disable CS0162 // 到達できないコードが検出されました
                return phash_version;
#pragma warning restore CS0162 // 到達できないコードが検出されました
            }
            Console.WriteLine(phash_version, "pHash 0.9.4");
            return phash_version;
        }

        //#ifdef HAVE_IMAGE_HASH
        //int ph_radon_projections(const CImg<byte> &img, int N, Projections &projs)
        //{
        //    int width = img.width();
        //    int height = img.height();
        //    int D = (width > height) ? width : height;
        //    float x_center = (float)width / 2;
        //    float y_center = (float)height / 2;
        //    int x_off = (int)std::floor(x_center + ROUNDING_FACTOR(x_center));
        //    int y_off = (int)std::floor(y_center + ROUNDING_FACTOR(y_center));

        //    projs.R = new CImg<byte>(N, D, 1, 1, 0);
        //    projs.nb_pix_perline = (int*)calloc(N, sizeof(int));

        //    if (!projs.R || !projs.nb_pix_perline)
        //        return EXIT_FAILURE;

        //    projs.size = N;

        //    CImg<byte>* ptr_radon_map = projs.R;
        //    int* nb_per_line = projs.nb_pix_perline;

        //    for (int k = 0; k < N / 4 + 1; k++)
        //    {
        //        double theta = k * cimg::Math.PI / N;
        //        double alpha = std::tan(theta);
        //        for (int x = 0; x < D; x++)
        //        {
        //            double y = alpha * (x - x_off);
        //            int yd = (int)std::floor(y + ROUNDING_FACTOR(y));
        //            if ((yd + y_off >= 0) && (yd + y_off < height) && (x < width))
        //            {
        //                *ptr_radon_map->data(k, x) = img(x, yd + y_off);
        //                nb_per_line[k] += 1;
        //            }
        //            if ((yd + x_off >= 0) && (yd + x_off < width) && (k != N / 4) && (x < height))
        //            {
        //                *ptr_radon_map->data(N / 2 - k, x) = img(yd + x_off, x);
        //                nb_per_line[N / 2 - k] += 1;
        //            }
        //        }
        //    }
        //    int j = 0;
        //    for (int k = 3 * N / 4; k < N; k++)
        //    {
        //        double theta = k * cimg::Math.PI / N;
        //        double alpha = std::tan(theta);
        //        for (int x = 0; x < D; x++)
        //        {
        //            double y = alpha * (x - x_off);
        //            int yd = (int)std::floor(y + ROUNDING_FACTOR(y));
        //            if ((yd + y_off >= 0) && (yd + y_off < height) && (x < width))
        //            {
        //                *ptr_radon_map->data(k, x) = img(x, yd + y_off);
        //                nb_per_line[k] += 1;
        //            }
        //            if ((y_off - yd >= 0) && (y_off - yd < width) && (2 * y_off - x >= 0) && (2 * y_off - x < height) && (k != 3 * N / 4))
        //            {
        //                *ptr_radon_map->data(k - j, x) = img(-yd + y_off, -(x - y_off) + y_off);
        //                nb_per_line[k - j] += 1;
        //            }

        //        }
        //        j += 2;
        //    }

        //    return EXIT_SUCCESS;

        //}
        //int ph_feature_vector(const Projections &projs, Features &fv)
        //{
        //    CImg<byte>* ptr_map = projs.R;
        //    CImg<byte> projection_map = *ptr_map;
        //    int* nb_perline = projs.nb_pix_perline;
        //    int N = projs.size;
        //    int D = projection_map.height();

        //    fv.features = (double*)malloc(N * sizeof(double));
        //    fv.size = N;
        //    if (!fv.features)
        //        return EXIT_FAILURE;

        //    double* feat_v = fv.features;
        //    double sum = 0.0;
        //    double sum_sqd = 0.0;
        //    for (int k = 0; k < N; k++)
        //    {
        //        double line_sum = 0.0;
        //        double line_sum_sqd = 0.0;
        //        int nb_pixels = nb_perline[k];
        //        for (int i = 0; i < D; i++)
        //        {
        //            line_sum += projection_map(k, i);
        //            line_sum_sqd += projection_map(k, i) * projection_map(k, i);
        //        }
        //        feat_v[k] = (line_sum_sqd / nb_pixels) - (line_sum * line_sum) / (nb_pixels * nb_pixels);
        //        sum += feat_v[k];
        //        sum_sqd += feat_v[k] * feat_v[k];
        //    }
        //    double mean = sum / N;
        //    double var = Math.Sqrt((sum_sqd / N) - (sum * sum) / (N * N));

        //    for (int i = 0; i < N; i++)
        //    {
        //        feat_v[i] = (feat_v[i] - mean) / var;
        //    }

        //    return EXIT_SUCCESS;
        //}
        //int ph_dct(const Features &fv, Digest &digest)
        //{
        //    int N = fv.size;
        //    const int nb_coeffs = 40;

        //    digest.coeffs = (byte*)malloc(nb_coeffs * sizeof(byte));
        //    if (!digest.coeffs)
        //        return EXIT_FAILURE;

        //    digest.size = nb_coeffs;

        //    double* R = fv.features;

        //    byte* D = digest.coeffs;

        //    double D_temp[nb_coeffs];
        //    double max = 0.0;
        //    double min = 0.0;
        //    for (int k = 0; k < nb_coeffs; k++)
        //    {
        //        double sum = 0.0;
        //        for (int n = 0; n < N; n++)
        //        {
        //            double temp = R[n] * Math.Cos((cimg::Math.PI * (2 * n + 1) * k) / (2 * N));
        //            sum += temp;
        //        }
        //        if (k == 0)
        //            D_temp[k] = sum / Math.Sqrt((double)N);
        //        else
        //            D_temp[k] = sum * SQRT_TWO / Math.Sqrt((double)N);
        //        if (D_temp[k] > max)
        //            max = D_temp[k];
        //        if (D_temp[k] < min)
        //            min = D_temp[k];
        //    }

        //    for (int i = 0; i < nb_coeffs; i++)
        //    {
        //        D[i] = (byte)(UCHAR_MAX * (D_temp[i] - min) / (max - min));

        //    }

        //    return EXIT_SUCCESS;
        //}

        //int ph_crosscorr(const Digest &x,const Digest &y, double &pcc, double threshold)
        //{
        //    int N = y.size;
        //    int result = 0;

        //    byte* x_coeffs = x.coeffs;
        //    byte* y_coeffs = y.coeffs;

        //    double* r = new double[N];
        //    double sumx = 0.0;
        //    double sumy = 0.0;
        //    for (int i = 0; i < N; i++)
        //    {
        //        sumx += x_coeffs[i];
        //        sumy += y_coeffs[i];
        //    }
        //    double meanx = sumx / N;
        //    double meany = sumy / N;
        //    double max = 0;
        //    for (int d = 0; d < N; d++)
        //    {
        //        double num = 0.0;
        //        double denx = 0.0;
        //        double deny = 0.0;
        //        for (int i = 0; i < N; i++)
        //        {
        //            num += (x_coeffs[i] - meanx) * (y_coeffs[(N + i - d) % N] - meany);
        //            denx += pow((x_coeffs[i] - meanx), 2);
        //            deny += pow((y_coeffs[(N + i - d) % N] - meany), 2);
        //        }
        //        r[d] = num / Math.Sqrt(denx * deny);
        //        if (r[d] > max)
        //            max = r[d];
        //    }
        //    delete[] r;
        //    pcc = max;
        //    if (max > threshold)
        //        result = 1;

        //    return result;
        //}

        //#ifdef max
        //#undef max
        //#endif

        //int _ph_image_digest(const CImg<byte> &img, double sigma, double gamma, Digest &digest, int N)
        //{
        //    int result = EXIT_FAILURE;
        //    CImg<byte> graysc;
        //    if (img.spectrum() >= 3)
        //    {
        //        graysc = img.get_RGBtoYCbCr().channel(0);
        //    }
        //    else if (img.spectrum() == 1)
        //    {
        //        graysc = img;
        //    }
        //    else
        //    {
        //        return result;
        //    }

        //    graysc.blur((float)sigma);

        //    (graysc / graysc.max()).pow(gamma);

        //    Projections projs;
        //    if (ph_radon_projections(graysc, N, projs) < 0)
        //        goto cleanup;

        //    Features features;
        //    if (ph_feature_vector(projs, features) < 0)
        //        goto cleanup;

        //    if (ph_dct(features, digest) < 0)
        //        goto cleanup;

        //    result = EXIT_SUCCESS;

        //    cleanup:
        //    free(projs.nb_pix_perline);
        //    free(features.features);

        //    delete projs.R;
        //    return result;
        //}

        //#define max(a,b) (((a)>(b))?(a):(b))

        //int ph_image_digest(const char* file, double sigma, double gamma, Digest &digest, int N)
        //{
        //    CImg<byte>* src = new CImg<byte>(file);
        //    int res = -1;
        //    if (src)
        //    {
        //        int result = _ph_image_digest(*src, sigma, gamma, digest, N);
        //        delete src;
        //        res = result;
        //    }
        //    return res;
        //}

        //int _ph_compare_images(const CImg<byte> &imA,const CImg<byte> &imB, double &pcc, double sigma, double gamma, int N, double threshold)
        //{
        //    int result = 0;
        //    Digest digestA;
        //    if (_ph_image_digest(imA, sigma, gamma, digestA, N) < 0)
        //        goto cleanup;

        //    Digest digestB;
        //    if (_ph_image_digest(imB, sigma, gamma, digestB, N) < 0)
        //        goto cleanup;

        //    if (ph_crosscorr(digestA, digestB, pcc, threshold) < 0)
        //        goto cleanup;

        //    if (pcc > threshold)
        //        result = 1;

        //    cleanup:

        //    free(digestA.coeffs);
        //    free(digestB.coeffs);
        //    return result;
        //}

        //int ph_compare_images(const char* file1, const char* file2, double &pcc, double sigma, double gamma, int N, double threshold)
        //{
        //    CImg<byte>* imA = new CImg<byte>(file1);
        //    CImg<byte>* imB = new CImg<byte>(file2);

        //    int res = _ph_compare_images(*imA, *imB, pcc, sigma, gamma, N, threshold);

        //    delete imA;
        //    delete imB;
        //    return res;
        //}

        /// <summary>
        /// return dct matrix, C Return DCT matrix of sqare size, N
        /// </summary>
        /// <param name="N">int denoting the size of the square matrix to create.</param>
        /// <returns>size NxN containing the dct matrix</returns>
        private static FloatImage ph_dct_matrix(int N)
        {
            var ptr_matrix = new FloatImage(N, N, 1 / (float)Math.Sqrt(N));
            var c1 = (float)Math.Sqrt(2f / N);
            for (int x = 0; x < N; x++)
            {
                for (int y = 1; y < N; y++)
                {
                    ptr_matrix[x, y] = c1 * (float)Math.Cos((Math.PI / 2 / N) * y * (2 * x + 1));
                }
            }
            return ptr_matrix;
        }
        //BinHash* _ph_bmb_new(uint32_t bytelength)
        //{
        //    BinHash* bh = (BinHash*)malloc(sizeof(BinHash));
        //    bh->bytelength = bytelength;
        //    bh->hash = (byte*)calloc(sizeof(byte), bytelength);
        //    bh->byteidx = 0;
        //    bh->bitmask = 128;
        //    return bh;
        //}

        //void ph_bmb_free(BinHash* bh)
        //{
        //    if (bh)
        //    {
        //        free(bh->hash);
        //        free(bh);
        //    }
        //}
        //int ph_bmb_imagehash(const char* file, byte method, BinHash** ret_hash)
        //{
        //    CImg<byte> img;
        //    const byte* ptrsrc;  // source pointer (img)
        //    byte* block;
        //    int pcol;  // "pointer" to pixel col (x)
        //    int prow;  // "pointer" to pixel row (y)
        //    int blockidx = 0;  //current idx of block begin processed.
        //    double median;  // median value of mean_vals
        //    const int preset_size_x = 256;
        //    const int preset_size_y = 256;
        //    const int blk_size_x = 16;
        //    const int blk_size_y = 16;
        //    int pixcolstep = blk_size_x;
        //    int pixrowstep = blk_size_y;

        //    int number_of_blocks;
        //    uint32_t bitsize;
        //    // number of bytes needed to store bitsize bits.
        //    uint32_t bytesize;

        //    if (!file || !ret_hash)
        //    {
        //        return -1;
        //    }
        //    try
        //    {
        //        img.load(file);
        //    }
        //    catch (CImgIOException ex)
        //    {
        //        return -1;
        //    }

        //    const int blk_size = blk_size_x * blk_size_y;
        //    block = (byte*)malloc(sizeof(byte) * blk_size);

        //    if (!block)
        //        return -1;

        //    switch (img.spectrum())
        //    {
        //        case 3: // from RGB
        //            img.RGBtoYCbCr().channel(0);
        //            break;
        //        default:
        //            *ret_hash = null;
        //            free(block);
        //            return -1;
        //    }

        //    img.resize(preset_size_x, preset_size_y);

        //    // ~step b
        //    ptrsrc = img.data();  // set pointer to beginning of pixel buffer

        //    if (method == 2)
        //    {
        //        pixcolstep /= 2;
        //        pixrowstep /= 2;

        //        number_of_blocks =
        //            ((preset_size_x / blk_size_x) * 2 - 1) *
        //            ((preset_size_y / blk_size_y) * 2 - 1);
        //    }
        //    else
        //    {
        //        number_of_blocks =
        //            preset_size_x / blk_size_x *
        //            preset_size_y / blk_size_y;
        //    }

        //    bitsize = number_of_blocks;
        //    bytesize = bitsize / 8;

        //    double* mean_vals = new double[number_of_blocks];

        //    /*
        //    * pixel row < block < block row < image
        //    *
        //    * The pixel rows of a block are copied consecutively
        //    * into the block buffer (using memcpy). When a block is
        //    * finished, the next block in the block row is processed.
        //    * After finishing a block row, the processing of the next
        //    * block row is started. An image consists of an arbitrary
        //    * number of block rows.
        //    */

        //    /* image (multiple rows of blocks) */
        //    for (prow = 0; prow <= preset_size_y - blk_size_y; prow += pixrowstep)
        //    {
        //        /* block row */
        //        for (pcol = 0; pcol <= preset_size_x - blk_size_x; pcol += pixcolstep)
        //        {
        //            // idx for array holding one block.
        //            int blockpos = 0;

        //            /* block */

        //            // i is used to address the different
        //            // pixel rows of a block
        //            for (int i = 0; i < blk_size_y; i++)
        //            {
        //                ptrsrc = img.data(pcol, prow + i);
        //                memcpy(block + blockpos, ptrsrc, blk_size_x);
        //                blockpos += blk_size_x;
        //            }

        //            mean_vals[blockidx] = CImg<byte>(block, blk_size).mean();
        //            blockidx++;

        //        }
        //    }

        //    /* calculate the median */
        //    median = CImg<double>(mean_vals, number_of_blocks).median();

        //    /* step e */
        //    BinHash* hash = _ph_bmb_new(bytesize);

        //    if (!hash)
        //    {
        //        *ret_hash = null;
        //        return -1;
        //    }

        //    *ret_hash = hash;
        //    for (uint32_t i = 0; i < bitsize; i++)
        //    {
        //        if (mean_vals[i] < median)
        //        {
        //            hash->addbit(0);
        //        }
        //        else
        //        {
        //            hash->addbit(1);
        //        }
        //    }
        //    delete[] mean_vals;
        //    free(block);
        //    return 0;
        //}

        // /*! /
        // *  /param 
        // *  /param 
        // *  /return 
        // */

        /// <summary>
        /// compute dct robust image hash
        /// </summary>
        /// <param name="file">file string variable for name of file</param>
        /// <param name="hash">hash of type ulong (must be 64-bit variable)</param>
        /// <returns>int value - -1 for failure, 1 for success</returns>
        public static ulong ph_dct_imagehash(Stream file)
        {
            var src = BitmapFrame.Create(file);

            var meanFilter = new FloatImage(7, 7, 1);

            FloatImage img;
            if (src.Format == PixelFormats.BlackWhite
                  || src.Format == PixelFormats.Gray2
                  || src.Format == PixelFormats.Gray4
                  || src.Format == PixelFormats.Gray8
                  || src.Format == PixelFormats.Gray16
                  || src.Format == PixelFormats.Gray32Float)
            {
                img = src.ToByteImage().Convolve(meanFilter);
            }
            else
            {
                img = src.ToByteImageOfY().Convolve(meanFilter);
            }

            var resized = img.Resize(32, 32);
            var C = ph_dct_matrix(32);
            var Ctransp = C.Transpose();
            var dctImage = C.Multiply(resized).Multiply(Ctransp);

            var sum = 0f;
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    sum += dctImage[x, y];
                }
            }

            var median = sum / 64f;
            var r = 0ul;
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    r |= dctImage[x, y] > median ? (1ul << (x + 8 * y)) : 0;
                }
            }

            return r;
        }

        //#ifdef HAVE_PTHREAD
        //void* ph_image_thread(void* p)
        //{
        //    slice* s = (slice*)p;
        //    for (int i = 0; i < s->n; ++i)
        //    {
        //        DP* dp = (DP*)s->hash_p[i];
        //        ulong hash;
        //        int ret = ph_dct_imagehash(dp->id, hash);
        //        dp->hash = (ulong*)malloc(sizeof(hash));
        //        memcpy(dp->hash, &hash, sizeof(hash));
        //        dp->hash_length = 1;
        //    }
        //}

        //DP** ph_dct_image_hashes(char* files[], int count, int threads)
        //{
        //    if (!files || count <= 0)
        //        return null;

        //    int num_threads;
        //    if (threads > count)
        //    {
        //        num_threads = count;
        //    }
        //    else if (threads > 0)
        //    {
        //        num_threads = threads;
        //    }
        //    else
        //    {
        //        num_threads = ph_num_threads();
        //    }

        //    DP** hashes = (DP**)malloc(count * sizeof(DP*));

        //    for (int i = 0; i < count; ++i)
        //    {
        //        hashes[i] = (DP*)malloc(sizeof(DP));
        //        hashes[i]->id = strdup(files[i]);
        //    }

        //    pthread_t thds[num_threads];

        //    int rem = count % num_threads;
        //    int start = 0;
        //    int off = 0;
        //    slice* s = new slice[num_threads];
        //    for (int n = 0; n < num_threads; ++n)
        //    {
        //        off = (int)floor((count / (float)num_threads) + (rem > 0 ? num_threads - (count % num_threads) : 0));

        //        s[n].hash_p = &hashes[start];
        //        s[n].n = off;
        //        s[n].hash_params = null;
        //        start += off;
        //        --rem;
        //        pthread_create(&thds[n], null, ph_image_thread, &s[n]);
        //    }
        //    for (int i = 0; i < num_threads; ++i)
        //    {
        //        pthread_join(thds[i], null);
        //    }
        //    delete[] s;

        //    return hashes;

        //}
        //#endif

        //#endif

        //#if defined(HAVE_VIDEO_HASH) && defined(HAVE_IMAGE_HASH)

        //CImgList<byte>* ph_getKeyFramesFromVideo(const char *filename){
        //    long N =  GetNumberVideoFrames(filename);

        //    if (N < 0){
        //        return null;
        //    }

        //    float frames_per_sec = 0.5*fps(filename);
        //    if (frames_per_sec < 0){
        //        return null;
        //    }

        //    int step = (int)(frames_per_sec + ROUNDING_FACTOR(frames_per_sec));
        //    long nbframes = (long)(N/step);

        //    float *dist = (float*)malloc((nbframes)*sizeof(float));
        //    if (!dist){
        //        return null;
        //    }
        //    CImg<float> prev(64,1,1,1,0);

        //    VFInfo st_info;
        //    st_info.filename = filename;
        //    st_info.nb_retrieval = 100;
        //    st_info.step = step;
        //    st_info.pixelformat = 0;
        //    st_info.pFormatCtx = null;
        //    st_info.width = -1;
        //    st_info.height = -1;

        //    CImgList<byte> *pframelist = new CImgList<byte>();
        //    if (!pframelist){
        //        return null;
        //    }
        //    int nbread = 0;
        //    int k=0;
        //    do {
        //        nbread = NextFrames(&st_info, pframelist);
        //        if (nbread < 0){
        //            delete pframelist;
        //            free(dist);
        //            return null;
        //        }
        //        unsigned int i = 0;
        //        while ((i < pframelist->size()) && (k < nbframes)){
        //            CImg<byte> current = pframelist->at(i++);
        //            CImg<float> hist = current.get_histogram(64,0,255);
        //            float d = 0.0;
        //            dist[k] = 0.0;
        //            cimg_forX(hist,X){
        //                d =  hist(X) - prev(X);
        //                d = (d>=0) ? d : -d;
        //                dist[k] += d;
        //                prev(X) = hist(X);
        //            }
        //            k++;
        //        }
        //        pframelist->clear();
        //    } while ((nbread >= st_info.nb_retrieval)&&(k < nbframes));
        //    vfinfo_close(&st_info);

        //    int S = 10;
        //    int L = 50;
        //    int alpha1 = 3;
        //    int alpha2 = 2;
        //    int s_begin, s_end;
        //    int l_begin, l_end;
        //    byte *bnds = (byte*)malloc(nbframes*sizeof(byte));
        //    if (!bnds){
        //        delete pframelist;
        //        free(dist);
        //        return null;
        //    }

        //    int nbboundaries = 0;
        //    k = 1;
        //    bnds[0] = 1;
        //    do {
        //        s_begin = (k-S >= 0) ? k-S : 0;
        //        s_end   = (k+S < nbframes) ? k+S : nbframes-1;
        //        l_begin = (k-L >= 0) ? k-L : 0;
        //        l_end   = (k+L < nbframes) ? k+L : nbframes-1;

        //        /* get global average */
        //        float ave_global, sum_global = 0.0, dev_global = 0.0;
        //        for (int i=l_begin;i<=l_end;i++){
        //            sum_global += dist[i];
        //        }
        //        ave_global = sum_global/((float)(l_end-l_begin+1));

        //        /*get global deviation */
        //        for (int i=l_begin;i<=l_end;i++){
        //            float dev = ave_global - dist[i];
        //            dev = (dev >= 0) ? dev : -1*dev;
        //            dev_global += dev;
        //        }
        //        dev_global = dev_global/((float)(l_end-l_begin+1));

        //        /* global threshold */
        //        float T_global = ave_global + alpha1*dev_global;

        //        /* get local maximum */
        //        int localmaxpos = s_begin;
        //        for (int i=s_begin;i<=s_end;i++){
        //            if (dist[i] > dist[localmaxpos])
        //                localmaxpos = i;
        //        }
        //        /* get 2nd local maximum */
        //        int localmaxpos2 = s_begin;
        //        float localmax2 = 0;
        //        for (int i=s_begin;i<=s_end;i++){
        //            if (i == localmaxpos)
        //                continue;
        //            if (dist[i] > localmax2){
        //                localmaxpos2 = i;
        //                localmax2 = dist[i];
        //            }
        //        }
        //        float T_local = alpha2*dist[localmaxpos2];
        //        float Thresh = (T_global >= T_local) ? T_global : T_local;

        //        if ((dist[k] == dist[localmaxpos])&&(dist[k] > Thresh)){
        //            bnds[k] = 1;
        //            nbboundaries++;
        //        }
        //        else {
        //            bnds[k] = 0;
        //        }
        //        k++;
        //    } while ( k < nbframes-1);
        //    bnds[nbframes-1]=1;
        //    nbboundaries += 2;

        //    int start = 0;
        //    int end = 0;
        //    int nbselectedframes = 0;
        //    do {
        //        /* find next boundary */
        //        do {end++;} while ((bnds[end]!=1)&&(end < nbframes));

        //        /* find min disparity within bounds */
        //        int minpos = start+1;
        //        for (int i=start+1; i < end;i++){
        //            if (dist[i] < dist[minpos])
        //                minpos = i;
        //        }
        //        bnds[minpos] = 2;
        //        nbselectedframes++;
        //        start = end;
        //    } while (start < nbframes-1);

        //    st_info.nb_retrieval = 1;
        //    st_info.width = 32;
        //    st_info.height = 32;
        //    k = 0;
        //    do {
        //        if (bnds[k]==2){
        //            if (ReadFrames(&st_info, pframelist, k*st_info.step,k*st_info.step + 1) < 0){
        //                delete pframelist;
        //                free(dist);
        //                return null;
        //            }
        //        }
        //        k++;
        //    } while (k < nbframes);
        //    vfinfo_close(&st_info);

        //    free(bnds);
        //    bnds = null;
        //    free(dist);
        //    dist = null;

        //    return pframelist;
        //}

        //ulong* ph_dct_videohash(const char *filename, int &Length){
        //    CImgList<byte> *keyframes = ph_getKeyFramesFromVideo(filename);
        //    if (keyframes == null)
        //        return null;

        //    Length = keyframes->size();

        //    ulong *hash = (ulong*)malloc(sizeof(ulong)*Length);
        //    CImg<float> *C = ph_dct_matrix(32);
        //    CImg<float> Ctransp = C->get_transpose();
        //    CImg<float> dctImage;
        //    CImg<float> subsec;
        //    CImg<byte> currentframe;

        //    for (unsigned int i=0;i < keyframes->size(); i++){
        //        currentframe = keyframes->at(i);
        //        currentframe.blur(1.0);
        //        dctImage = (*C)*(currentframe)*Ctransp;
        //        subsec = dctImage.crop(1,1,8,8).unroll('x');
        //        float med = subsec.median();
        //        hash[i] =     0x0000000000000000;
        //        ulong one = 0x0000000000000001;
        //        for (int j=0;j<64;j++){
        //            if (subsec(j) > med)
        //                hash[i] |= one;
        //            one = one << 1;
        //        }
        //    }

        //    keyframes->clear();
        //    delete keyframes;
        //    keyframes = null;
        //    delete C;
        //    C = null;
        //    return hash;
        //}

        //#ifdef HAVE_PTHREAD
        //void *ph_video_thread(void *p)
        //{
        //    slice *s = (slice *)p;
        //    for(int i = 0; i < s->n; ++i)
        //    {
        //        DP *dp = (DP *)s->hash_p[i];
        //        int N;
        //        ulong *hash = ph_dct_videohash(dp->id, N);
        //        if(hash)
        //        {
        //            dp->hash = hash;
        //            dp->hash_length = N;
        //        }
        //        else
        //        {
        //            dp->hash = null;
        //            dp->hash_length = 0;
        //        }
        //    }
        //}

        //DP** ph_dct_video_hashes(char *files[], int count, int threads)
        //{
        //    if(!files || count <= 0)
        //        return null;

        //    int num_threads;
        //    if(threads > count)
        //    {
        //        num_threads = count;
        //    }
        //    else if(threads > 0)
        //    {
        //        num_threads = threads;
        //    }
        //    else
        //    {
        //        num_threads = ph_num_threads();
        //    }

        //    DP **hashes = (DP**)malloc(count*sizeof(DP*));

        //    for(int i = 0; i < count; ++i)
        //    {
        //        hashes[i] = (DP *)malloc(sizeof(DP));
        //        hashes[i]->id = strdup(files[i]);
        //    }

        //    pthread_t thds[num_threads];

        //    int rem = count % num_threads;
        //    int start = 0;
        //    int off = 0;
        //    slice *s = new slice[num_threads];
        //    for(int n = 0; n < num_threads; ++n)
        //    {
        //        off = (int)floor((count/(float)num_threads) + (rem>0?num_threads-(count % num_threads):0));

        //        s[n].hash_p = &hashes[start];
        //        s[n].n = off;
        //        s[n].hash_params = null;
        //        start += off;
        //        --rem;
        //        pthread_create(&thds[n], null, ph_video_thread, &s[n]);
        //    }
        //    for(int i = 0; i < num_threads; ++i)
        //    {
        //        pthread_join(thds[i], null);
        //    }
        //    delete[] s;

        //    return hashes;

        //}
        //#endif

        //double ph_dct_videohash_dist(ulong* hashA, int N1, ulong* hashB, int N2, int threshold)
        //{
        //    int den = (N1 <= N2) ? N1 : N2;
        //    int C[N1 + 1][N2+1];

        //    for (int i = 0; i<N1+1;i++){
        //        C[i][0] = 0;
        //    }
        //    for (int j = 0; j<N2+1;j++){
        //        C[0][j] = 0;
        //    }
        //    for (int i = 1; i<N1+1;i++){
        //        for (int j = 1; j<N2+1;j++){
        //            int d = ph_hamming_distance(hashA[i - 1], hashB[j - 1]);
        //            if (d <= threshold){
        //                C[i][j] = C[i - 1][j - 1] + 1;
        //            } else {
        //                C[i][j] = ((C[i - 1][j] >= C[i][j - 1])) ? C[i - 1][j] : C[i][j - 1];
        //            }
        //        }
        //    }

        //    double result = (double)(C[N1][N2]) / (double)(den);

        //    return result;
        //}

        //#endif

        public static int ph_hamming_distance(ulong hash1, ulong hash2)
        {
            ulong x = hash1 ^ hash2;
            const ulong m1 = 0x5555555555555555UL;
            const ulong m2 = 0x3333333333333333UL;
            const ulong h01 = 0x0101010101010101UL;
            const ulong m4 = 0x0f0f0f0f0f0f0f0fUL;
            x -= (x >> 1) & m1;
            x = (x & m2) + ((x >> 2) & m2);
            x = (x + (x >> 4)) & m4;
            return (int)((x * h01) >> 56);
        }

        //#ifdef HAVE_IMAGE_HASH

        ///*
        //DP** ph_read_imagehashes(const char *dirname,int pathlength, int &count){
        //count = 0;
        //struct dirent *dir_entry;
        //DIR *dir = opendir(dirname);
        //if (!dir)
        //return null;

        //while ((dir_entry = readdir(dir)) != 0){
        //if (strcmp(dir_entry->d_name,".")&& strcmp(dir_entry->d_name,"..")){
        //count++;
        //}
        //}

        //DP **hashlist = (DP**)malloc(count*sizeof(DP**));
        //if (!hashlist)
        //{
        //closedir(dir);
        //return null;
        //}

        //DP *dp = null;
        //int index = 0;
        //errno = 0;
        //ulong tmphash = 0;
        //char path[100];
        //path[0] = '\0';
        //rewinddir(dir);
        //while ((dir_entry = readdir(dir)) != 0){
        //if (strcmp(dir_entry->d_name,".") && strcmp(dir_entry->d_name,"..")){
        //strcat(path, dirname);
        //strcat(path, "/");
        //strcat(path, dir_entry->d_name);
        //if (ph_dct_imagehash(path, tmphash) < 0)  //calculate the hash
        //continue;
        //dp = ph_malloc_datapoint(UINT64ARRAY);
        //dp->id = strdup(path);
        //dp->hash = (void*)&tmphash;
        //dp->hash_length = 1;
        //hashlist[index++] = dp;
        //}
        //errno = 0;
        //path[0]='\0';
        //}

        //closedir(dir);
        //return hashlist;

        //}
        //*/
        //CImg<float>* GetMHKernel(float alpha, float level)
        //{
        //    int sigma = (int)(4 * pow((float)alpha, (float)level));
        //    static CImg<float>* pkernel = null;
        //    float xpos, ypos, A;
        //    if (!pkernel)
        //    {
        //        pkernel = new CImg<float>(2 * sigma + 1, 2 * sigma + 1, 1, 1, 0);
        //        cimg_forXY(*pkernel, X, Y){
        //            xpos = pow(alpha, -level) * (X - sigma);
        //            ypos = pow(alpha, -level) * (Y - sigma);
        //            A = xpos * xpos + ypos * ypos;
        //            pkernel->atXY(X, Y) = (2 - A) * exp(-A / 2);
        //        }
        //    }
        //    return pkernel;
        //}

        //byte* ph_mh_imagehash(const char* filename, int &N, float alpha, float lvl)
        //{
        //    if (filename == null)
        //    {
        //        return null;
        //    }
        //    byte* hash = (unsigned char*)malloc(72 * sizeof(byte));
        //    N = 72;

        //    CImg<byte> src(filename);
        //    CImg<byte> img;

        //    if (src.spectrum() == 3)
        //    {
        //        img = src.get_RGBtoYCbCr().channel(0).blur(1.0).resize(512, 512, 1, 1, 5).get_equalize(256);
        //    }
        //    else
        //    {
        //        img = src.channel(0).get_blur(1.0).resize(512, 512, 1, 1, 5).get_equalize(256);
        //    }
        //    src.clear();

        //    CImg<float>* pkernel = GetMHKernel(alpha, lvl);
        //    CImg<float> fresp = img.get_correlate(*pkernel);
        //    img.clear();
        //    fresp.normalize(0, 1.0);
        //    CImg<float> blocks(31, 31, 1, 1, 0);
        //    for (int rindex = 0; rindex < 31; rindex++)
        //    {
        //        for (int cindex = 0; cindex < 31; cindex++)
        //        {
        //            blocks(rindex, cindex) = (float)fresp.get_crop(rindex * 16, cindex * 16, rindex * 16 + 16 - 1, cindex * 16 + 16 - 1).sum();
        //        }
        //    }
        //    int hash_index;
        //    int nb_ones = 0, nb_zeros = 0;
        //    int bit_index = 0;
        //    unsigned char hashbyte = 0;
        //    for (int rindex = 0; rindex < 31 - 2; rindex += 4)
        //    {
        //        CImg<float> subsec;
        //        for (int cindex = 0; cindex < 31 - 2; cindex += 4)
        //        {
        //            subsec = blocks.get_crop(cindex, rindex, cindex + 2, rindex + 2).unroll('x');
        //            float ave = (float)subsec.mean();
        //            cimg_forX(subsec, I){
        //                hashbyte <<= 1;
        //                if (subsec(I) > ave)
        //                {
        //                    hashbyte |= 0x01;
        //                    nb_ones++;
        //                }
        //                else
        //                {
        //                    nb_zeros++;
        //                }
        //                bit_index++;
        //                if ((bit_index % 8) == 0)
        //                {
        //                    hash_index = (int)(bit_index / 8) - 1;
        //                    hash[hash_index] = hashbyte;
        //                    hashbyte = 0x00;
        //                }
        //            }
        //        }
        //    }

        //    return hash;
        //}
        //#endif

        //char** ph_readfilenames(const char* dirname, int &count)
        //{
        //    count = 0;
        //    struct dirent * dir_entry;
        //DIR* dir = opendir(dirname);
        //    if (!dir)
        //        return null;

        //    /*count files */
        //    while ((dir_entry = readdir(dir)) != null){
        //        if (strcmp(dir_entry->d_name, ".") && strcmp(dir_entry->d_name,".."))
        //            count++;
        //    }

        //    /* alloc list of files */
        //    char** files = (char**)malloc(count * sizeof(*files));
        //    if (!files)
        //        return null;

        //    errno = 0;
        //    int index = 0;
        //char path[1024];
        //path[0] = '\0';
        //    rewinddir(dir);
        //    while ((dir_entry = readdir(dir)) != 0){
        //        if (strcmp(dir_entry->d_name,".") && strcmp(dir_entry->d_name,"..")){
        //            strcat(path, dirname);
        //            strcat(path, "/");
        //            strcat(path, dir_entry->d_name);
        //files[index++] = strdup(path);
        //        }
        //        path[0]='\0';
        //    }
        //    if (errno)
        //        return null;
        //    closedir(dir);
        //    return files;
        //}

        /// <summary>
        /// brief count number bits set in given byte.
        /// </summary>
        /// <param name="val">byte value.</param>
        /// <returns>int value for number of bits set.</returns>
        public static int ph_bitcount8(byte val)
        {
            int num = 0;
            while (val != 0)
            {
                ++num;
                val = (byte)(val & (val - 1));
            }
            return num;
        }

        /// <summary>
        /// brief compute hamming distance between two byte arrays
        /// </summary>
        /// <param name="hashA">byte array for first hash</param>
        /// <param name="lenA">int length of hashA</param>
        /// <param name="hashB">byte array for second hash</param>
        /// <param name="lenB">int length of hashB</param>
        /// <returns>double value for normalized hamming distance.</returns>
        public unsafe static double ph_hammingdistance2(byte* hashA, int lenA, byte* hashB, int lenB)
        {
            if (lenA != lenB)
            {
                return -1.0;
            }
            if ((hashA == null) || (hashB == null) || (lenA <= 0))
            {
                return -1.0;
            }
            double dist = 0;
            byte D = 0;
            for (int i = 0; i < lenA; i++)
            {
                D = (byte)(hashA[i] ^ hashB[i]);
                dist = dist + ph_bitcount8(D);
            }
            double bits = (double)lenA * 8;
            return dist / bits;
        }

        //TxtHashPoint* ph_texthash(const char* filename, int* nbpoints)
        //{
        //    int count;
        //    TxtHashPoint* TxtHash = null;
        //    TxtHashPoint WinHash[WindowLength];
        //    char kgram[KgramLength];

        //    FILE* pfile = fopen(filename, "r");
        //    if (!pfile)
        //    {
        //        return null;
        //    }
        //    struct stat fileinfo;
        //    fstat(fileno(pfile),&fileinfo);
        //    count = fileinfo.st_size - WindowLength + 1;
        //    count = (int)(0.01* count);
        //    int d;
        //ulong hashword = 0ULL;

        //    TxtHash = (TxtHashPoint*)malloc(count*sizeof(struct ph_hash_point));
        //    if (!TxtHash){
        //        return null;
        //    }
        //    * nbpoints = 0;
        //int i, first = 0, last = KgramLength - 1;
        //int text_index = 0;
        //int win_index = 0;
        //    for (i=0;i<KgramLength;i++){    /* calc first kgram */
        //        d = fgetc(pfile);
        //        if (d == EOF){
        //            free(TxtHash);
        //            return null;
        //        }
        //        if (d <= 47)         /*skip cntrl chars*/
        //            continue;
        //        if ( ((d >= 58)&&(d <= 64)) || ((d >= 91)&&(d <= 96)) || (d >= 123) ) /*skip punct*/
        //            continue;
        //        if ((d >= 65)&&(d<=90))       /*convert upper to lower case */
        //            d = d + 32;

        //        kgram[i] = (char)d;
        //        hashword = hashword << delta;   /* rotate left or shift left ??? */
        //        hashword = hashword^textkeys[d];/* right now, rotate breaks it */
        //    }

        //    WinHash[win_index].hash = hashword;
        //    WinHash[win_index++].index = text_index;
        //    struct ph_hash_point minhash;
        //    minhash.hash = ULLONG_MAX;
        //    minhash.index = 0;
        //    struct ph_hash_point prev_minhash;
        //    prev_minhash.hash = ULLONG_MAX;
        //    prev_minhash.index = 0;

        //    while ((d=fgetc(pfile)) != EOF){    /*remaining kgrams */
        //        text_index++;
        //        if (d == EOF){
        //            free(TxtHash);
        //            return null;
        //        }
        //        if (d <= 47)         /*skip cntrl chars*/
        //            continue;
        //        if ( ((d >= 58)&&(d <= 64)) || ((d >= 91)&&(d <= 96)) || (d >= 123) ) /*skip punct*/
        //            continue;
        //        if ((d >= 65)&&(d<=90))       /*convert upper to lower case */
        //            d = d + 32;

        //        ulong oldsym = textkeys[kgram[first % KgramLength]];

        ///* rotate or left shift ??? */
        ///* right now, rotate breaks it */
        //oldsym = oldsym << delta* KgramLength;
        //hashword = hashword << delta;
        //        hashword = hashword^textkeys[d];
        //        hashword = hashword^oldsym;
        //        kgram[last % KgramLength] = (char)d;
        //        first++;
        //        last++;

        //        WinHash[win_index % WindowLength].hash = hashword;
        //        WinHash[win_index % WindowLength].index = text_index;
        //        win_index++;

        //        if (win_index >= WindowLength){
        //            minhash.hash = ULLONG_MAX;
        //            for (i=win_index;i<win_index+WindowLength;i++){
        //                if (WinHash[i % WindowLength].hash <= minhash.hash){
        //                    minhash.hash = WinHash[i % WindowLength].hash;
        //                    minhash.index = WinHash[i % WindowLength].index;
        //                }
        //            }
        //            if (minhash.hash != prev_minhash.hash){
        //                TxtHash[(*nbpoints)].hash = minhash.hash;
        //                TxtHash[(*nbpoints)++].index = minhash.index;
        //                prev_minhash.hash = minhash.hash;
        //                prev_minhash.index = minhash.index;

        //            } else {
        //                TxtHash[*nbpoints].hash = prev_minhash.hash;
        //                TxtHash[(*nbpoints)++].index = prev_minhash.index;
        //            }
        //            win_index = 0;
        //        }
        //    }

        //    fclose(pfile);
        //    return TxtHash;
        //}

        //TxtMatch* ph_compare_text_hashes(TxtHashPoint* hash1, int N1, TxtHashPoint* hash2, int N2, int* nbmatches)
        //{
        //    int max_matches = (N1 >= N2) ? N1 : N2;
        //    TxtMatch* found_matches = (TxtMatch*)malloc(max_matches * sizeof(TxtMatch));
        //    if (!found_matches)
        //    {
        //        return null;
        //    }

        //    *nbmatches = 0;
        //    int i, j;
        //    for (i = 0; i < N1; i++)
        //    {
        //        for (j = 0; j < N2; j++)
        //        {
        //            if (hash1[i].hash == hash2[j].hash)
        //            {
        //                int m = i + 1;
        //                int n = j + 1;
        //                int cnt = 1;
        //                while ((m < N1) && (n < N2) && (hash1[m++].hash == hash2[n++].hash))
        //                {
        //                    cnt++;
        //                }
        //                found_matches[*nbmatches].first_index = i;
        //                found_matches[*nbmatches].second_index = j;
        //                found_matches[*nbmatches].length = cnt;
        //                (*nbmatches)++;
        //            }
        //        }
        //    }
        //    return found_matches;
        //}
    }
}