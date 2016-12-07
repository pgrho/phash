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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash
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
    //typedef unsigned _uint64 ulong64;
    //typedef signed _int64 long64;
    //#else
    //typedef unsigned long long ulong64;
    //typedef signed long long long64;
    //typedef unsigned char uint8_t;
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
    //    uint8_t fileno;
    //}
    //FileIndex;


    ///* structure for a single hash */
    //typedef struct ph_datapoint
    //{
    //    char* id;
    //    void* hash;
    //    float* path;
    //    uint32_t hash_length;
    //    uint8_t hash_type;
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
    //    uint8_t* hash;
    //    uint32_t bytelength;
    //    uint32_t byteidx; // used by addbit()
    //    uint8_t bitmask;  // used by addbit()

    //    /*
    //	 * add a single bit to hash. the bits are 
    //	 * written from left to right.
    //	 */
    //    int addbit(uint8_t bit)
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

    ///*! /brief Radon Projection info
    // */
    //#ifdef HAVE_IMAGE_HASH
    //typedef struct ph_projections
    //{
    //    CImg<uint8_t>* R;           //contains projections of image of angled lines through center
    //    int* nb_pix_perline;        //the head of int array denoting the number of pixels of each line
    //    int size;                   //the size of nb_pix_perline
    //}
    //Projections;
    //#endif

    ///*! /brief feature vector info
    // */
    //typedef struct ph_feature_vector
    //{
    //    double* features;           //the head of the feature array of double's
    //    int size;                   //the size of the feature array
    //}
    //Features;

    ///*! /brief Digest info
    // */
    //typedef struct ph_digest
    //{
    //    char* id;                   //hash id
    //    uint8_t* coeffs;            //the head of the digest integer coefficient array
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
    //    ulong64 hash;
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

    ///*! /brief copyright information
    // */
    //const char* ph_about();

    ///*! /brief radon function
    // *  Find radon projections of N lines running through the image center for lines angled 0
    // *  to 180 degrees from horizontal.
    // *  /param img - CImg src image
    // *  /param  N  - int number of angled lines to consider.
    // *  /param  projs - (out) Projections struct 
    // *  /return int value - less than 0 for error
    // */
    //#ifdef HAVE_IMAGE_HASH
    //int ph_radon_projections(const CImg<uint8_t> &img, int N, Projections &projs);

    ///*! /brief feature vector
    // *         compute the feature vector from a radon projection map.
    // *  /param  projs - Projections struct
    // *  /param  fv    - (out) Features struct
    // *  /return int value - less than 0 for error
    //*/
    //int ph_feature_vector(const Projections &projs, Features &fv);

    ///*! /brief dct 
    // *  Compute the dct of a given vector
    // *  /param R - vector of input series
    // *  /param D - (out) the dct of R
    // *  /return  int value - less than 0 for error
    //*/
    //int ph_dct(const Features &fv, Digest &digest);

    ///*! /brief cross correlation for 2 series
    // *  Compute the cross correlation of two series vectors
    // *  /param x - Digest struct
    // *  /param y - Digest struct
    // *  /param pcc - double value the peak of cross correlation
    // *  /param threshold - double value for the threshold value for which 2 images
    // *                     are considered the same or different.
    // *  /return - int value - 1 (true) for same, 0 (false) for different, < 0 for error
    // */

    //int ph_crosscorr(const Digest &x,const Digest &y, double &pcc, double threshold = 0.90);

    ///*! /brief image digest
    // *  Compute the image digest for an image given the input image
    // *  /param img - CImg object representing an input image
    // *  /param sigma - double value for the deviation for a gaussian filter function 
    // *  /param gamma - double value for gamma correction on the input image
    // *  /param digest - (out) Digest struct
    // *  /param N      - int value for the number of angles to consider. 
    // *  /return       - less than 0 for error
    // */
    //int _ph_image_digest(const CImg<uint8_t> &img, double sigma, double gamma, Digest &digest, int N = 180);

    ///*! /brief image digest
    // *  Compute the image digest given the file name.
    // *  /param file - string value for file name of input image.
    // *  /param sigma - double value for the deviation for gaussian filter
    // *  /param gamma - double value for gamma correction on the input image.
    // *  /param digest - Digest struct
    // *  /param N      - int value for number of angles to consider
    // */
    //int ph_image_digest(const char* file, double sigma, double gamma, Digest &digest, int N = 180);


    ///*! /brief compare 2 images
    // *  /param imA - CImg object of first image 
    // *  /param imB - CImg object of second image
    // *  /param pcc   - (out) double value for peak of cross correlation
    // *  /param sigma - double value for the deviation of gaussian filter
    // *  /param gamma - double value for gamma correction of images
    // *  /param N     - int number for the number of angles of radon projections
    // *  /param theshold - double value for the threshold
    // *  /return int 0 (false) for different images, 1 (true) for same image, less than 0 for error
    // */
    //int _ph_compare_images(const CImg<uint8_t> &imA,const CImg<uint8_t> &imB, double &pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90);

    ///*! /brief compare 2 images
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

    ///*! /brief return dct matrix, C
    // *  Return DCT matrix of sqare size, N
    // *  /param N - int denoting the size of the square matrix to create.
    // *  /return CImg<double> size NxN containing the dct matrix
    // */
    //static CImg<float>* ph_dct_matrix(const int N);

    ///*! /brief compute dct robust image hash
    // *  /param file string variable for name of file
    // *  /param hash of type ulong64 (must be 64-bit variable)
    // *  /return int value - -1 for failure, 1 for success
    // */
    //int ph_dct_imagehash(const char* file, ulong64 &hash);

    //int ph_bmb_imagehash(const char* file, uint8_t method, BinHash** ret_hash);
    //#endif

    //#ifdef HAVE_PTHREAD
    //DP** ph_dct_image_hashes(char* files[], int count, int threads = 0);
    //#endif

    //#ifdef HAVE_VIDEO_HASH
    //static CImgList<uint8_t>* ph_getKeyFramesFromVideo(const char* filename);

    //ulong64* ph_dct_videohash(const char* filename, int &Length);

    //DP** ph_dct_video_hashes(char* files[], int count, int threads = 0);

    //double ph_dct_videohash_dist(ulong64* hashA, int N1, ulong64* hashB, int N2, int threshold = 21);
    //#endif

    ///* ! /brief dct video robust hash
    // *   Compute video hash based on the dct of normalized video 32x32x64 cube
    // *   /param file name of file
    // *   /param hash ulong64 value for hash value
    // *   /return int value - less than 0 for error
    // */
    //#ifdef HAVE_IMAGE_HASH
    //int ph_hamming_distance(const ulong64 hash1,const ulong64 hash2);

    ///** /brief create a list of datapoint's directly from a directory of image files
    // *  /param dirname - path and name of directory containg all image file names
    // *  /param capacity - int value for upper limit on number of hashes
    // *  /param count - number of hashes created (out param)
    // *  /return pointer to a list of DP pointers (NULL for error)
    // */

    //DP** ph_read_imagehashes(const char* dirname, int capacity, int &count);

    ///** /brief create MH image hash for filename image
    //*   /param filename - string name of image file
    //*   /param N - (out) int value for length of image hash returned
    //*   /param alpha - int scale factor for marr wavelet (default=2)
    //*   /param lvl   - int level of scale factor (default = 1)
    //*   /return uint8_t array
    //**/
    //uint8_t* ph_mh_imagehash(const char* filename, int &N, float alpha = 2.0f, float lvl = 1.0f);
    //#endif
    ///** /brief count number bits set in given byte
    //*   /param val - uint8_t byte value
    //*   /return int value for number of bits set
    //**/
    //int ph_bitcount8(uint8_t val);

    ///** /brief compute hamming distance between two byte arrays
    // *  /param hashA - byte array for first hash
    // *  /param lenA - int length of hashA 
    // *  /param hashB - byte array for second hash
    // *  /param lenB - int length of hashB
    // *  /return double value for normalized hamming distance
    // **/
    //double ph_hammingdistance2(uint8_t* hashA, int lenA, uint8_t* hashB, int lenB);

    ///** /brief get all the filenames in specified directory
    // *  /param dirname - string value for path and filename
    // *  /param cap - int value for upper limit to number of files
    // *  /param count - int value for number of file names returned
    // *  /return array of pointers to string file names (NULL for error)
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

    ///* random char mapping for textual hash */

    //static const ulong64 textkeys[256] = {
    //    15498727785010036736ULL,
    //    7275080914684608512ULL,
    //    14445630958268841984ULL,
    //    14728618948878663680ULL,
    //    16816925489502355456ULL,
    //    3644179549068984320ULL,
    //    6183768379476672512ULL,
    //    14171334718745739264ULL,
    //    5124038997949022208ULL,
    //    10218941994323935232ULL,
    //    8806421233143906304ULL,
    //    11600620999078313984ULL,
    //    6729085808520724480ULL,
    //    9470575193177980928ULL,
    //    17565538031497117696ULL,
    //    16900815933189128192ULL,
    //    11726811544871239680ULL,
    //    13231792875940872192ULL,
    //    2612106097615437824ULL,
    //    11196599515807219712ULL,
    //    300692472869158912ULL,
    //    4480470094610169856ULL,
    //    2531475774624497664ULL,
    //    14834442768343891968ULL,
    //    2890219059826130944ULL,
    //    7396118625003765760ULL,
    //    2394211153875042304ULL,
    //    2007168123001634816ULL,
    //    18426904923984625664ULL,
    //    4026129272715345920ULL,
    //    9461932602286931968ULL,
    //    15478888635285110784ULL,
    //    11301210195989889024ULL,
    //    5460819486846222336ULL,
    //    11760763510454222848ULL,
    //    9671391611782692864ULL,
    //    9104999035915206656ULL,
    //    17944531898520829952ULL,
    //    5395982256818880512ULL,
    //    14229038033864228864ULL,
    //    9716729819135213568ULL,
    //    14202403489962786816ULL,
    //    7382914959232991232ULL,
    //    16445815627655938048ULL,
    //    5226234609431216128ULL,
    //    6501708925610491904ULL,
    //    14899887495725449216ULL,
    //    16953046154302455808ULL,
    //    1286757727841812480ULL,
    //    17511993593340887040ULL,
    //    9702901604990058496ULL,
    //    1587450200710971392ULL,
    //    3545719622831439872ULL,
    //    12234377379614556160ULL,
    //    16421892977644797952ULL,
    //    6435938682657570816ULL,
    //    1183751930908770304ULL,
    //    369360057810288640ULL,
    //    8443106805659205632ULL,
    //    1163912781183844352ULL,
    //    4395489330525634560ULL,
    //    17905039407946137600ULL,
    //    16642801425058889728ULL,
    //    15696699526515523584ULL,
    //    4919114829672742912ULL,
    //    9956820861803560960ULL,
    //    6921347064588664832ULL,
    //    14024113865587949568ULL,
    //    9454608686614839296ULL,
    //    12317329321407545344ULL,
    //    9806407834332561408ULL,
    //    724594440630435840ULL,
    //    8072988737660780544ULL,
    //    17189322793565552640ULL,
    //    17170410068286373888ULL,
    //    13299223355681931264ULL,
    //    5244287645466492928ULL,
    //    13623553490302271488ULL,
    //    11805525436274835456ULL,
    //    6531045381898240000ULL,
    //    12688803018523541504ULL,
    //    3061682967555342336ULL,
    //    8118495582609211392ULL,
    //    16234522641354981376ULL,
    //    15296060347169898496ULL,
    //    6093644486544457728ULL,
    //    4223717250303000576ULL,
    //    16479812286668603392ULL,
    //    6463004544354746368ULL,
    //    12666824055962206208ULL,
    //    17643725067852447744ULL,
    //    10858493883470315520ULL,
    //    12125119390198792192ULL,
    //    15839782419201785856ULL,
    //    8108449336276287488ULL,
    //    17044234219871535104ULL,
    //    7349859215885729792ULL,
    //    15029796409454886912ULL,
    //    12621604020339867648ULL,
    //    16804467902500569088ULL,
    //    8900381657152880640ULL,
    //    3981267780962877440ULL,
    //    17529062343131004928ULL,
    //    16973370403403595776ULL,
    //    2723846500818878464ULL,
    //    16252728346297761792ULL,
    //    11825849685375975424ULL,
    //    7968134154875305984ULL,
    //    11429537762890481664ULL,
    //    5184631047941259264ULL,
    //    14499179536773545984ULL,
    //    5671596707704471552ULL,
    //    8246314024086536192ULL,
    //    4170931045673205760ULL,
    //    3459375275349901312ULL,
    //    5095630297546883072ULL,
    //    10264575540807598080ULL,
    //    7683092525652901888ULL,
    //    3128698510505934848ULL,
    //    16727580085162344448ULL,
    //    1903172507905556480ULL,
    //    2325679513238765568ULL,
    //    9139329894923108352ULL,
    //    14028291906694283264ULL,
    //    18165461932440551424ULL,
    //    17247779239789330432ULL,
    //    12625782052856266752ULL,
    //    7068577074616729600ULL,
    //    13830831575534665728ULL,
    //    6800641999486582784ULL,
    //    5426300911997681664ULL,
    //    4284469158977994752ULL,
    //    10781909780449460224ULL,
    //    4508619181419134976ULL,
    //    2811095488672038912ULL,
    //    13505756289858273280ULL,
    //    2314603454007345152ULL,
    //    14636945174048014336ULL,
    //    3027146371024027648ULL,
    //    13744141225487761408ULL,
    //    1374832156869656576ULL,
    //    17526325907797573632ULL,
    //    968993859482681344ULL,
    //    9621146180956192768ULL,
    //    3250512879761227776ULL,
    //    4428369143422517248ULL,
    //    14716776478503075840ULL,
    //    13515088420568825856ULL,
    //    12111461669075419136ULL,
    //    17845474997598945280ULL,
    //    11795924440611553280ULL,
    //    14014634185570910208ULL,
    //    1724410437128159232ULL,
    //    2488510261825110016ULL,
    //    9596182018555641856ULL,
    //    1443128295859159040ULL,
    //    1289545427904888832ULL,
    //    3775219997702356992ULL,
    //    8511705379065823232ULL,
    //    15120377003439554560ULL,
    //    10575862005778874368ULL,
    //    13938006291063504896ULL,
    //    958102097297932288ULL,
    //    2911027712518782976ULL,
    //    18446625472482639872ULL,
    //    3769197585969971200ULL,
    //    16416784002377056256ULL,
    //    2314484861370368000ULL,
    //    18406142768607920128ULL,
    //    997186299691532288ULL,
    //    16058626086858129408ULL,
    //    1334230851768025088ULL,
    //    76768133779554304ULL,
    //    17027619946340810752ULL,
    //    10955377032724217856ULL,
    //    3327281022130716672ULL,
    //    3009245016053776384ULL,
    //    7225409437517742080ULL,
    //    16842369442699542528ULL,
    //    15120706693719130112ULL,
    //    6624140361407135744ULL,
    //    10191549809601544192ULL,
    //    10688596805580488704ULL,
    //    8348550798535294976ULL,
    //    12680060080016588800ULL,
    //    1838034750426578944ULL,
    //    9791679102984388608ULL,
    //    13969605507921477632ULL,
    //    5613254748128935936ULL,
    //    18303384482050211840ULL,
    //    10643238446241415168ULL,
    //    16189116753907810304ULL,
    //    13794646699404165120ULL,
    //    11601340543539347456ULL,
    //    653400401306976256ULL,
    //    13794528098177253376ULL,
    //    15370538129509318656ULL,
    //    17070184403684032512ULL,
    //    16109012959547621376ULL,
    //    15329936824407687168ULL,
    //    18067370711965499392ULL,
    //    13720894972696199168ULL,
    //    16664167676175712256ULL,
    //    18144138845745053696ULL,
    //    12301770853917392896ULL,
    //    9172800635190378496ULL,
    //    3024675794166218752ULL,
    //    15311015869971169280ULL,
    //    16398210081298055168ULL,
    //    1420301171746144256ULL,
    //    11984978489980747776ULL,
    //    4575606368995639296ULL,
    //    11611850981347688448ULL,
    //    4226831221851684864ULL,
    //    12924157176120868864ULL,
    //    5845166987654725632ULL,
    //    6064865972278263808ULL,
    //    4269092205395705856ULL,
    //    1368028430456586240ULL,
    //    11678120728997134336ULL,
    //    4125732613736366080ULL,
    //    12011266876698001408ULL,
    //    9420493409195393024ULL,
    //    17920379313140531200ULL,
    //    5165863346527797248ULL,
    //    10073893810502369280ULL,
    //    13268163337608232960ULL,
    //    2089657402327564288ULL,
    //    8697334149066784768ULL,
    //    10930432232036237312ULL,
    //    17419594235325186048ULL,
    //    8317960787322732544ULL,
    //    6204583131022884864ULL,
    //    15637017837791346688ULL,
    //    8015355559358234624ULL,
    //    59609911230726144ULL,
    //    6363074407862108160ULL,
    //    11040031362114387968ULL,
    //    15370625789791830016ULL,
    //    4314540415450611712ULL,
    //    12460332533860532224ULL,
    //    8908860206063026176ULL,
    //    8890146784446251008ULL,
    //    5625439441498669056ULL,
    //    13135691436504645632ULL,
    //    3367559886857568256ULL,
    //    11470606437743329280ULL,
    //    753813335073357824ULL,
    //    7636652092253274112ULL,
    //    12838634868199915520ULL,
    //    12431934064070492160ULL,
    //    11762384705989640192ULL,
    //    6403157671188365312ULL,
    //    3405683408146268160ULL,
    //    11236019945420619776ULL,
    //    11569021017716162560ULL
    //};

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

    //int ph_num_threads()
    //{
    //    int numCPU = 1;
    //#ifdef __GLIBC__
    //    numCPU = sysconf(_SC_NPROCESSORS_ONLN);
    //#else
    //    int mib[2];
    //    size_t len;

    //    mib[0] = CTL_HW;
    //    mib[1] = HW_AVAILCPU;

    //    sysctl(mib, 2, &numCPU, &len, NULL, 0);

    //    if (numCPU < 1)
    //    {
    //        mib[1] = HW_NCPU;
    //        sysctl(mib, 2, &numCPU, &len, NULL, 0);

    //        if (numCPU < 1)
    //        {
    //            numCPU = 1;
    //        }
    //    }

    //#endif
    //    return numCPU;
    //}
    //#endif

    //const char phash_project[] = "%s. Copyright 2008-2010 Aetilius, Inc.";
    //char phash_version[255] = { 0 };
    //const char* ph_about(){
    //    if(phash_version[0] != 0)
    //        return phash_version;

    //    snprintf(phash_version, sizeof(phash_version), phash_project, "pHash 0.9.4");
    //    return phash_version;
    //}
    //#ifdef HAVE_IMAGE_HASH
    //int ph_radon_projections(const CImg<uint8_t> &img, int N, Projections &projs)
    //{

    //    int width = img.width();
    //    int height = img.height();
    //    int D = (width > height) ? width : height;
    //    float x_center = (float)width / 2;
    //    float y_center = (float)height / 2;
    //    int x_off = (int)std::floor(x_center + ROUNDING_FACTOR(x_center));
    //    int y_off = (int)std::floor(y_center + ROUNDING_FACTOR(y_center));

    //    projs.R = new CImg<uint8_t>(N, D, 1, 1, 0);
    //    projs.nb_pix_perline = (int*)calloc(N, sizeof(int));

    //    if (!projs.R || !projs.nb_pix_perline)
    //        return EXIT_FAILURE;

    //    projs.size = N;

    //    CImg<uint8_t>* ptr_radon_map = projs.R;
    //    int* nb_per_line = projs.nb_pix_perline;

    //    for (int k = 0; k < N / 4 + 1; k++)
    //    {
    //        double theta = k * cimg::PI / N;
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
    //        double theta = k * cimg::PI / N;
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

    //    CImg<uint8_t>* ptr_map = projs.R;
    //    CImg<uint8_t> projection_map = *ptr_map;
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
    //    double var = sqrt((sum_sqd / N) - (sum * sum) / (N * N));

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

    //    digest.coeffs = (uint8_t*)malloc(nb_coeffs * sizeof(uint8_t));
    //    if (!digest.coeffs)
    //        return EXIT_FAILURE;

    //    digest.size = nb_coeffs;

    //    double* R = fv.features;

    //    uint8_t* D = digest.coeffs;

    //    double D_temp[nb_coeffs];
    //    double max = 0.0;
    //    double min = 0.0;
    //    for (int k = 0; k < nb_coeffs; k++)
    //    {
    //        double sum = 0.0;
    //        for (int n = 0; n < N; n++)
    //        {
    //            double temp = R[n] * Math.Cos((cimg::PI * (2 * n + 1) * k) / (2 * N));
    //            sum += temp;
    //        }
    //        if (k == 0)
    //            D_temp[k] = sum / sqrt((double)N);
    //        else
    //            D_temp[k] = sum * SQRT_TWO / sqrt((double)N);
    //        if (D_temp[k] > max)
    //            max = D_temp[k];
    //        if (D_temp[k] < min)
    //            min = D_temp[k];
    //    }

    //    for (int i = 0; i < nb_coeffs; i++)
    //    {

    //        D[i] = (uint8_t)(UCHAR_MAX * (D_temp[i] - min) / (max - min));

    //    }

    //    return EXIT_SUCCESS;
    //}

    //int ph_crosscorr(const Digest &x,const Digest &y, double &pcc, double threshold)
    //{

    //    int N = y.size;
    //    int result = 0;

    //    uint8_t* x_coeffs = x.coeffs;
    //    uint8_t* y_coeffs = y.coeffs;

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
    //        r[d] = num / sqrt(denx * deny);
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

    //int _ph_image_digest(const CImg<uint8_t> &img, double sigma, double gamma, Digest &digest, int N)
    //{

    //    int result = EXIT_FAILURE;
    //    CImg<uint8_t> graysc;
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

    //    CImg<uint8_t>* src = new CImg<uint8_t>(file);
    //    int res = -1;
    //    if (src)
    //    {
    //        int result = _ph_image_digest(*src, sigma, gamma, digest, N);
    //        delete src;
    //        res = result;
    //    }
    //    return res;
    //}

    //int _ph_compare_images(const CImg<uint8_t> &imA,const CImg<uint8_t> &imB, double &pcc, double sigma, double gamma, int N, double threshold)
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

    //    CImg<uint8_t>* imA = new CImg<uint8_t>(file1);
    //    CImg<uint8_t>* imB = new CImg<uint8_t>(file2);

    //    int res = _ph_compare_images(*imA, *imB, pcc, sigma, gamma, N, threshold);

    //    delete imA;
    //    delete imB;
    //    return res;
    //}

    //CImg<float>* ph_dct_matrix(const int N)
    //{
    //    CImg<float>* ptr_matrix = new CImg<float>(N, N, 1, 1, 1 / sqrt((float)N));
    //    const float c1 = sqrt(2.0f / N);
    //    for (int x = 0; x < N; x++)
    //    {
    //        for (int y = 1; y < N; y++)
    //        {
    //            *ptr_matrix->data(x, y) = c1 * (float)Math.Cos((cimg::PI / 2 / N) * y * (2 * x + 1));
    //        }
    //    }
    //    return ptr_matrix;
    //}
    //BinHash* _ph_bmb_new(uint32_t bytelength)
    //{
    //    BinHash* bh = (BinHash*)malloc(sizeof(BinHash));
    //    bh->bytelength = bytelength;
    //    bh->hash = (uint8_t*)calloc(sizeof(uint8_t), bytelength);
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
    //int ph_bmb_imagehash(const char* file, uint8_t method, BinHash** ret_hash)
    //{
    //    CImg<uint8_t> img;
    //    const uint8_t* ptrsrc;  // source pointer (img)
    //    uint8_t* block;
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
    //    block = (uint8_t*)malloc(sizeof(uint8_t) * blk_size);

    //    if (!block)
    //        return -1;

    //    switch (img.spectrum())
    //    {
    //        case 3: // from RGB
    //            img.RGBtoYCbCr().channel(0);
    //            break;
    //        default:
    //            *ret_hash = NULL;
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

    //            mean_vals[blockidx] = CImg<uint8_t>(block, blk_size).mean();
    //            blockidx++;

    //        }
    //    }

    //    /* calculate the median */
    //    median = CImg<double>(mean_vals, number_of_blocks).median();

    //    /* step e */
    //    BinHash* hash = _ph_bmb_new(bytesize);

    //    if (!hash)
    //    {
    //        *ret_hash = NULL;
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

    //int ph_dct_imagehash(const char* file, ulong64 &hash)
    //{

    //    if (!file)
    //    {
    //        return -1;
    //    }
    //    CImg<uint8_t> src;
    //    try
    //    {
    //        src.load(file);
    //    }
    //    catch (CImgIOException ex)
    //    {
    //        return -1;
    //    }
    //    CImg<float> meanfilter(7, 7, 1, 1, 1);
    //    CImg<float> img;
    //    if (src.spectrum() == 3)
    //    {
    //        img = src.RGBtoYCbCr().channel(0).get_convolve(meanfilter);
    //    }
    //    else if (src.spectrum() == 4)
    //    {
    //        int width = img.width();
    //        int height = img.height();
    //        int depth = img.depth();
    //        img = src.crop(0, 0, 0, 0, width - 1, height - 1, depth - 1, 2).RGBtoYCbCr().channel(0).get_convolve(meanfilter);
    //    }
    //    else
    //    {
    //        img = src.channel(0).get_convolve(meanfilter);
    //    }

    //    img.resize(32, 32);
    //    CImg<float>* C = ph_dct_matrix(32);
    //    CImg<float> Ctransp = C->get_transpose();

    //    CImg<float> dctImage = (*C) * img * Ctransp;

    //    CImg<float> subsec = dctImage.crop(1, 1, 8, 8).unroll('x'); ;

    //    float median = subsec.median();
    //    ulong64 one = 0x0000000000000001;
    //    hash = 0x0000000000000000;
    //    for (int i = 0; i < 64; i++)
    //    {
    //        float current = subsec(i);
    //        if (current > median)
    //            hash |= one;
    //        one = one << 1;
    //    }

    //    delete C;

    //    return 0;
    //}

    //#ifdef HAVE_PTHREAD
    //void* ph_image_thread(void* p)
    //{
    //    slice* s = (slice*)p;
    //    for (int i = 0; i < s->n; ++i)
    //    {
    //        DP* dp = (DP*)s->hash_p[i];
    //        ulong64 hash;
    //        int ret = ph_dct_imagehash(dp->id, hash);
    //        dp->hash = (ulong64*)malloc(sizeof(hash));
    //        memcpy(dp->hash, &hash, sizeof(hash));
    //        dp->hash_length = 1;
    //    }
    //}

    //DP** ph_dct_image_hashes(char* files[], int count, int threads)
    //{
    //    if (!files || count <= 0)
    //        return NULL;

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
    //        s[n].hash_params = NULL;
    //        start += off;
    //        --rem;
    //        pthread_create(&thds[n], NULL, ph_image_thread, &s[n]);
    //    }
    //    for (int i = 0; i < num_threads; ++i)
    //    {
    //        pthread_join(thds[i], NULL);
    //    }
    //    delete[] s;

    //    return hashes;

    //}
    //#endif


    //#endif

    //#if defined(HAVE_VIDEO_HASH) && defined(HAVE_IMAGE_HASH)


    //CImgList<uint8_t>* ph_getKeyFramesFromVideo(const char *filename){

    //    long N =  GetNumberVideoFrames(filename);

    //    if (N < 0){
    //        return NULL;
    //    }

    //    float frames_per_sec = 0.5*fps(filename);
    //    if (frames_per_sec < 0){
    //        return NULL;
    //    }

    //    int step = (int)(frames_per_sec + ROUNDING_FACTOR(frames_per_sec));
    //    long nbframes = (long)(N/step);

    //    float *dist = (float*)malloc((nbframes)*sizeof(float));
    //    if (!dist){
    //        return NULL;
    //    }
    //    CImg<float> prev(64,1,1,1,0);

    //    VFInfo st_info;
    //    st_info.filename = filename;
    //    st_info.nb_retrieval = 100;
    //    st_info.step = step;
    //    st_info.pixelformat = 0;
    //    st_info.pFormatCtx = NULL;
    //    st_info.width = -1;
    //    st_info.height = -1;

    //    CImgList<uint8_t> *pframelist = new CImgList<uint8_t>();
    //    if (!pframelist){
    //        return NULL;
    //    }
    //    int nbread = 0;
    //    int k=0;
    //    do {
    //        nbread = NextFrames(&st_info, pframelist);
    //        if (nbread < 0){
    //            delete pframelist;
    //            free(dist);
    //            return NULL;
    //        }
    //        unsigned int i = 0;
    //        while ((i < pframelist->size()) && (k < nbframes)){
    //            CImg<uint8_t> current = pframelist->at(i++);
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
    //    uint8_t *bnds = (uint8_t*)malloc(nbframes*sizeof(uint8_t));
    //    if (!bnds){
    //        delete pframelist;
    //        free(dist);
    //        return NULL;
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
    //                return NULL;
    //            }
    //        }
    //        k++;
    //    } while (k < nbframes);
    //    vfinfo_close(&st_info);

    //    free(bnds);
    //    bnds = NULL;
    //    free(dist);
    //    dist = NULL;

    //    return pframelist;
    //}


    //ulong64* ph_dct_videohash(const char *filename, int &Length){

    //    CImgList<uint8_t> *keyframes = ph_getKeyFramesFromVideo(filename);
    //    if (keyframes == NULL)
    //        return NULL;

    //    Length = keyframes->size();

    //    ulong64 *hash = (ulong64*)malloc(sizeof(ulong64)*Length);
    //    CImg<float> *C = ph_dct_matrix(32);
    //    CImg<float> Ctransp = C->get_transpose();
    //    CImg<float> dctImage;
    //    CImg<float> subsec;
    //    CImg<uint8_t> currentframe;

    //    for (unsigned int i=0;i < keyframes->size(); i++){
    //        currentframe = keyframes->at(i);
    //        currentframe.blur(1.0);
    //        dctImage = (*C)*(currentframe)*Ctransp;
    //        subsec = dctImage.crop(1,1,8,8).unroll('x');
    //        float med = subsec.median();
    //        hash[i] =     0x0000000000000000;
    //        ulong64 one = 0x0000000000000001;
    //        for (int j=0;j<64;j++){
    //            if (subsec(j) > med)
    //                hash[i] |= one;
    //            one = one << 1;
    //        }
    //    }

    //    keyframes->clear();
    //    delete keyframes;
    //    keyframes = NULL;
    //    delete C;
    //    C = NULL;
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
    //        ulong64 *hash = ph_dct_videohash(dp->id, N);
    //        if(hash)
    //        {
    //            dp->hash = hash;
    //            dp->hash_length = N;
    //        }
    //        else
    //        {
    //            dp->hash = NULL;
    //            dp->hash_length = 0;
    //        }
    //    }
    //}

    //DP** ph_dct_video_hashes(char *files[], int count, int threads)
    //{
    //    if(!files || count <= 0)
    //        return NULL;

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
    //        s[n].hash_params = NULL;
    //        start += off;
    //        --rem;
    //        pthread_create(&thds[n], NULL, ph_video_thread, &s[n]);
    //    }
    //    for(int i = 0; i < num_threads; ++i)
    //    {
    //        pthread_join(thds[i], NULL);
    //    }
    //    delete[] s;

    //    return hashes;

    //}
    //#endif

    //double ph_dct_videohash_dist(ulong64* hashA, int N1, ulong64* hashB, int N2, int threshold)
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

    //int ph_hamming_distance(const ulong64 hash1,const ulong64 hash2)
    //{
    //    ulong64 x = hash1 ^ hash2;
    //    const ulong64 m1 = 0x5555555555555555ULL;
    //    const ulong64 m2 = 0x3333333333333333ULL;
    //    const ulong64 h01 = 0x0101010101010101ULL;
    //    const ulong64 m4 = 0x0f0f0f0f0f0f0f0fULL;
    //    x -= (x >> 1) & m1;
    //    x = (x & m2) + ((x >> 2) & m2);
    //    x = (x + (x >> 4)) & m4;
    //    return (x * h01) >> 56;
    //}


    //#ifdef HAVE_IMAGE_HASH

    ///*
    //DP** ph_read_imagehashes(const char *dirname,int pathlength, int &count){

    //count = 0;
    //struct dirent *dir_entry;
    //DIR *dir = opendir(dirname);
    //if (!dir)
    //return NULL;

    //while ((dir_entry = readdir(dir)) != 0){
    //if (strcmp(dir_entry->d_name,".")&& strcmp(dir_entry->d_name,"..")){
    //count++;
    //}
    //}

    //DP **hashlist = (DP**)malloc(count*sizeof(DP**));
    //if (!hashlist)
    //{
    //closedir(dir);
    //return NULL;
    //}

    //DP *dp = NULL;
    //int index = 0;
    //errno = 0;
    //ulong64 tmphash = 0;
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
    //    static CImg<float>* pkernel = NULL;
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

    //uint8_t* ph_mh_imagehash(const char* filename, int &N, float alpha, float lvl)
    //{
    //    if (filename == NULL)
    //    {
    //        return NULL;
    //    }
    //    uint8_t* hash = (unsigned char*)malloc(72 * sizeof(uint8_t));
    //    N = 72;

    //    CImg<uint8_t> src(filename);
    //    CImg<uint8_t> img;

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
    //        return NULL;

    //    /*count files */
    //    while ((dir_entry = readdir(dir)) != NULL){
    //        if (strcmp(dir_entry->d_name, ".") && strcmp(dir_entry->d_name,".."))
    //            count++;
    //    }

    //    /* alloc list of files */
    //    char** files = (char**)malloc(count * sizeof(*files));
    //    if (!files)
    //        return NULL;

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
    //        return NULL;
    //    closedir(dir);
    //    return files;
    //}


    //int ph_bitcount8(uint8_t val)
    //{
    //    int num = 0;
    //    while (val)
    //    {
    //        ++num;
    //        val &= val - 1;
    //    }
    //    return num;
    //}



    //double ph_hammingdistance2(uint8_t* hashA, int lenA, uint8_t* hashB, int lenB)
    //{
    //    if (lenA != lenB)
    //    {
    //        return -1.0;
    //    }
    //    if ((hashA == NULL) || (hashB == NULL) || (lenA <= 0))
    //    {
    //        return -1.0;
    //    }
    //    double dist = 0;
    //    uint8_t D = 0;
    //    for (int i = 0; i < lenA; i++)
    //    {
    //        D = hashA[i] ^ hashB[i];
    //        dist = dist + (double)ph_bitcount8(D);
    //    }
    //    double bits = (double)lenA * 8;
    //    return dist / bits;

    //}

    //TxtHashPoint* ph_texthash(const char* filename, int* nbpoints)
    //{
    //    int count;
    //    TxtHashPoint* TxtHash = NULL;
    //    TxtHashPoint WinHash[WindowLength];
    //    char kgram[KgramLength];

    //    FILE* pfile = fopen(filename, "r");
    //    if (!pfile)
    //    {
    //        return NULL;
    //    }
    //    struct stat fileinfo;
    //    fstat(fileno(pfile),&fileinfo);
    //    count = fileinfo.st_size - WindowLength + 1;
    //    count = (int)(0.01* count);
    //    int d;
    //ulong64 hashword = 0ULL;

    //    TxtHash = (TxtHashPoint*)malloc(count*sizeof(struct ph_hash_point));
    //    if (!TxtHash){
    //        return NULL;
    //    }
    //    * nbpoints = 0;
    //int i, first = 0, last = KgramLength - 1;
    //int text_index = 0;
    //int win_index = 0;
    //    for (i=0;i<KgramLength;i++){    /* calc first kgram */
    //        d = fgetc(pfile);
    //        if (d == EOF){
    //            free(TxtHash);
    //            return NULL;
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
    //            return NULL;
    //        }
    //        if (d <= 47)         /*skip cntrl chars*/
    //            continue;
    //        if ( ((d >= 58)&&(d <= 64)) || ((d >= 91)&&(d <= 96)) || (d >= 123) ) /*skip punct*/
    //            continue;
    //        if ((d >= 65)&&(d<=90))       /*convert upper to lower case */
    //            d = d + 32;

    //        ulong64 oldsym = textkeys[kgram[first % KgramLength]];

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
    //        return NULL;
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