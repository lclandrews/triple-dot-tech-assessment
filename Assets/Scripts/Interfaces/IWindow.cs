using System.Collections;

using TripleDot.Blur;

namespace TripleDot.Interfaces
{
    public interface IWindow
    {
        public IEnumerator Show(BlurBehaviour blurBehaviour);
        public IEnumerator Hide(BlurBehaviour blurBehaviour);
    }
}
