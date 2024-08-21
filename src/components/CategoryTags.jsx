import { categoryTags } from "../constants/constants";

const CategoryTags = () => {
  return (
    <div className="flex flex-wrap gap-3 mt-3 w-full justify-center" >
        {/* For extra small screens (default) */}
        {categoryTags.slice(0, 4).map((tag, index) => (
            <div key={index} className="sm:hidden bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                <p>{tag}</p>
            </div>
        ))}
        {/* For small-large screens */}
        {categoryTags.slice(0, 5).map((tag, index) => (
            <div key={index} className="hidden sm:block lg:hidden bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                <p>{tag}</p>
            </div>
        ))}
        {/* For large screens and above */}
        {categoryTags.map((tag, index) => (
            <div key={index} className="hidden lg:block bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                <p>{tag}</p>
            </div>
        ))}
    </div>
  )
}

export default CategoryTags